using EduardTeodor.Application.UseCases;
using EduardTeodor.Presentation.Requests;
using EduardTeodor.Presentation.Responses;

namespace EduardTeodor.Presentation.Endpoints;

public class PostEndpoints
{
    public static void MapPostEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/posts").WithTags("posts");

        group.MapGet("/", GetAllPublishedPosts);
        group.MapGet("/{slug}", GetPublishedPost);
    }

    private static async Task<IResult> GetAllPublishedPosts(
        GetAllPublishedPosts useCase,
        [AsParameters] PostQuery query)
    {
        var posts = await useCase.ExecuteAsync(query.PostType, query.Starred);
        var response = posts.Select(post => new PostSummarisedResponse(
            post.Slug,
            post.Title,
            post.PostType,
            post.Summary,
            post.Starred,
            post.TagNames
        ));

        return Results.Ok(response);
    }

    private static async Task<IResult> GetPublishedPost(
        GetPublishedPost useCase,
        string slug)
    {
        var post = await useCase.ExecuteAsync(slug);

        return post is null ? Results.NotFound() 
            : Results.Ok(new PostResponse(
                post.Slug,
                post.Title,
                post.PostType,
                post.Summary,
                post.Description,
                post.Starred,
                post.TagNames
            ));
    }
}