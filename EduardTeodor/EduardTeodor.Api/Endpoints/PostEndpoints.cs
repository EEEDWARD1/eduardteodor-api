using EduardTeodor.Api.Models;
using EduardTeodor.Api.Services;

namespace EduardTeodor.Api.Endpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/posts", async (
            PostService service,
            PostType? postType,
            string? tag,
            CancellationToken cancellationToken) =>
        {
            if (postType.HasValue && !Enum.IsDefined(postType.Value))
            {
                return Results.BadRequest("Invalid post type.");
            }

            var posts = await service.GetAllAsync(postType, tag, cancellationToken);
            return Results.Ok(posts);
        })
        .WithName("GetPosts")
        .WithTags("Posts");

        app.MapGet("/posts/{slug}", async (
            string slug,
            PostService service,
            CancellationToken cancellationToken) =>
        {
            var post = await service.GetBySlugAsync(slug, cancellationToken);
            return post is null ? Results.NotFound() : Results.Ok(post);
        })
        .WithName("GetPostBySlug")
        .WithTags("Posts");

        return app;
    }
}
