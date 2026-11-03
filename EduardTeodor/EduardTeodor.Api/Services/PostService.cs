using EduardTeodor.Api.Contracts;
using EduardTeodor.Api.Data;
using EduardTeodor.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EduardTeodor.Api.Services;

public class PostService(AppDbContext db)
{
    public Task<PostResponse?> GetBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default)
    {
        return ProjectPosts(db.Posts.AsNoTracking().Where(post => post.Slug == slug))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<PostResponse>> GetAllAsync(
        PostType? postType,
        string? tag,
        CancellationToken cancellationToken = default)
    {
        var query = db.Posts.AsNoTracking();

        if (postType.HasValue)
        {
            query = query.Where(post => post.PostType == postType.Value);
        }

        if (!string.IsNullOrWhiteSpace(tag))
        {
            var tagName = tag.Trim();
            query = query.Where(post => post.Tags.Any(t => t.Name == tagName));
        }

        return await ProjectPosts(query.OrderBy(post => post.Id))
            .ToListAsync(cancellationToken);
    }

    private static IQueryable<PostResponse> ProjectPosts(IQueryable<Post> query)
    {
        return query.Select(post => new PostResponse(
                post.Id,
                post.PostType,
                post.Slug,
                post.Title,
                post.Description,
                post.Image,
                post.Content,
                post.Href,
                post.Tags.OrderBy(t => t.Name).Select(t => t.Name).ToList()));
    }
}
