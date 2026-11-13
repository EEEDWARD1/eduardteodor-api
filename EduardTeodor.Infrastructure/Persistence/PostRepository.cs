using Dapper;
using EduardTeodor.Application.Interfaces;
using EduardTeodor.Domain.Entities;
using Npgsql;

namespace EduardTeodor.Infrastructure.Persistence;

public class PostRepository(NpgsqlDataSource dataSource) : IPostRepository
{
    public async Task<IEnumerable<Post>> GetAllPublishedPostsAsync(int? postType, bool? starred)
    {
        await using var conn = await dataSource.OpenConnectionAsync();

        var sql = """
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
                    JOIN public.tags t on t.id = pt.tag_id
                    WHERE pt.post_id = p.id
                    ORDER by t.tag_name
                ) AS TagNames
                FROM public.posts p
                WHERE p.published = true
                    AND (@PostType::integer IS NULL OR p.post_type = @PostType)
                    AND (@Starred::boolean IS NULL OR p.starred = @Starred)
                ORDER BY p.created_at DESC;
            """;

        return await conn.QueryAsync<Post>(sql, new { PostType = postType, Starred = starred });

    }

    public async Task<Post?> GetPublishedPostAsync(string slug)
    {
        await using var conn = await dataSource.OpenConnectionAsync();

        var sql = """
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
            WHERE p.published = true
                AND p.slug = @Slug;
            """;

        return await conn.QuerySingleOrDefaultAsync<Post>(sql, new { Slug = slug });
    }
}
