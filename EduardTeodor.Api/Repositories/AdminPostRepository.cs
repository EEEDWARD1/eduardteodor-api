using Dapper;
using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Models;
using Npgsql;

namespace EduardTeodor.Api.Repositories;

public class AdminPostRepository(NpgsqlDataSource dataSource) : IAdminPostRepository
{
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