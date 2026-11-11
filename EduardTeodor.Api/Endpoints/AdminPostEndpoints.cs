using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Services;

namespace EduardTeodor.Api.Endpoints;

public static class AdminPostEndpoints
{
    public static void MapAdminPostEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/admin/posts");

        group.MapGet("/", GetAllPostsAsync);
        //group.MapGet("/{slug}", GetBySlugAsync);
        //group.MapPut("/", CreateNewPostAsync);
    }

    private static async Task<IResult> GetAllPostsAsync(
        IAdminPostService service,
        [AsParameters] PostFilter filters)
    {
        if (filters.PostType is < 1 or > 3)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(PostFilter.PostType)] = ["PostType must be between 1 and 3."]
            });
        }

        var posts = await service.GetAllPostsAsync(filters);

        return Results.Ok(posts);
    }
}
