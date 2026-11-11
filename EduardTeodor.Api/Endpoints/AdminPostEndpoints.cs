using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Services;
using System.ComponentModel.DataAnnotations;

namespace EduardTeodor.Api.Endpoints;

public static class AdminPostEndpoints
{
    public static void MapAdminPostEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/admin/posts");

        group.MapGet("/", GetAllPostsAsync);
        group.MapPost("/", CreatePostAsync);
        group.MapDelete("/{slug}", DeletePostAsync);
    }

    private static async Task<IResult> CreatePostAsync(
        IAdminPostService service,
        CreatePostRequest request)
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(request, new ValidationContext(request), validationResults, true);
        var errors = validationResults
            .SelectMany(result => result.MemberNames.Select(name => new { Name = name, Message = result.ErrorMessage! }))
            .GroupBy(error => error.Name)
            .ToDictionary(group => group.Key, group => group.Select(error => error.Message).ToArray());

        if (request.TagNames is not null && request.TagNames.Any(tag => string.IsNullOrWhiteSpace(tag) || tag.Length > 255))
        {
            errors[nameof(request.TagNames)] = ["Each tag must contain between 1 and 255 characters and cannot contain only whitespace."];
        }

        if (errors.Count > 0)
        {
            return Results.ValidationProblem(errors);
        }

        var post = await service.CreatePostAsync(request);
        return post is null
            ? Results.Conflict(new { Message = "A post with this slug already exists." })
            : Results.Created((string?)null, post);
    }

    private static async Task<IResult> DeletePostAsync(IAdminPostService service, string slug)
    {
        return await service.DeletePostAsync(slug) ? Results.NoContent() : Results.NotFound();
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
