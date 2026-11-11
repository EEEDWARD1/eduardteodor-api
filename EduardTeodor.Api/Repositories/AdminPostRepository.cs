using Dapper;
using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Models;
using Npgsql;

namespace EduardTeodor.Api.Repositories;

public class AdminPostRepository(NpgsqlDataSource dataSource) : IAdminPostRepository
{
    public async Task<Post?> CreatePostAsync(CreatePostRequest request)
    {
        await using var conn = await dataSource.OpenConnectionAsync();
        await using var transaction = await conn.BeginTransactionAsync();

        const string sql = """
            INSERT INTO public.posts (slug, title, post_type, summary, description, published, starred)
            VALUES (@Slug, @Title, @PostType, @Summary, @Description, @Published, @Starred)
            ON CONFLICT (slug) DO NOTHING
            RETURNING id, slug, title, post_type AS PostType, summary, description,
                      published, starred, created_at AS CreatedAt, updated_at AS UpdatedAt;
            """;

        var post = await conn.QuerySingleOrDefaultAsync<Post>(sql, request, transaction);
        if (post is null)
        {
            return null;
        }

        var tags = request.TagNames.Distinct(StringComparer.Ordinal).Order(StringComparer.Ordinal).ToArray();
        foreach (var tag in tags)
        {
            const string tagSql = """
                INSERT INTO public.tags (tag_name) VALUES (@Tag)
                ON CONFLICT (tag_name) DO UPDATE SET tag_name = EXCLUDED.tag_name
                RETURNING id;
                """;
            var tagId = await conn.QuerySingleAsync<int>(tagSql, new { Tag = tag }, transaction);

            const string linkSql = """
                INSERT INTO public.post_tags (post_id, tag_id) VALUES (@PostId, @TagId);
                """;
            await conn.ExecuteAsync(linkSql, new { PostId = post.Id, TagId = tagId }, transaction);
        }

        await transaction.CommitAsync();
        post.TagNames = tags;
        return post;
    }

    public async Task<bool> DeletePostAsync(string slug)
    {
        await using var conn = await dataSource.OpenConnectionAsync();
        const string sql = "DELETE FROM public.posts WHERE slug = @Slug;";
        return await conn.ExecuteAsync(sql, new { Slug = slug }) > 0;
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync(PostFilter parameters)
    {
        await using var conn = await dataSource.OpenConnectionAsync();

        const string sql = """
        SELECT
            p.id,
            p.slug,
            p.title,
            p.post_type AS PostType,
            p.summary,
            p.description,
            p.published,
            p.starred,
            p.created_at AS CreatedAt,
            p.updated_at AS UpdatedAt,
            ARRAY(
                SELECT t.tag_name
                FROM public.post_tags pt
                JOIN public.tags t ON t.id = pt.tag_id
                WHERE pt.post_id = p.id
                ORDER BY t.tag_name
            ) AS TagNames
        FROM public.posts p
        WHERE (@PostType IS NULL OR p.post_type = @PostType)
          AND (@Starred IS NULL OR p.starred = @Starred)
        ORDER BY p.created_at DESC;
        """;

        return await conn.QueryAsync<Post>(
            sql,
            new
            {
                parameters.PostType,
                parameters.Starred
            }
        );
    }
}
