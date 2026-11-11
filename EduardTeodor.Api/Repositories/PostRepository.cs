using EduardTeodor.Api.Models;
using Npgsql;
using Dapper;
using EduardTeodor.Api.DTOs.Posts;

namespace EduardTeodor.Api.Repositories;

public class PostRepository(NpgsqlDataSource dataSource) : IPostRepository
{
    public async Task<IEnumerable<Post>> GetAllPublishedAsync(
    PostFilter parameters)
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
        WHERE p.published = true
          AND (@PostType IS NULL OR p.post_type = @PostType)
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

    public async Task<Post?> GetPublishedPostBySlugAsync(string slug)
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
            WHERE p.published = true AND p.slug = @slug
            ORDER BY p.created_at DESC;
        """;

        return await conn.QuerySingleOrDefaultAsync<Post>(sql, new { Slug = slug });
    }
}
