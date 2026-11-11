using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Services;

namespace EduardTeodor.Api.Endpoints;

public static class PostEndpoints
{
    public static void MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/posts");

        group.MapGet("/", GetAllPublished);
        group.MapGet("/{slug}", GetPublishedBySlugAsync);
    }

    private static async Task<IResult> GetAllPublished(
        IPostService postService, 
        [AsParameters] PostFilter parameters)
    {
        var posts = await postService.GetAllPublishedAsync(parameters);

        return Results.Ok(posts);
    }

    private static async Task<IResult> GetPublishedBySlugAsync(IPostService postService, string slug)
    {
        var post = await postService.GetPublishedBySlugAsync(slug);

        return post is not null ? Results.Ok(post) : Results.NotFound();
    }
}
