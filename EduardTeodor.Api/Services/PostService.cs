using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Repositories;

namespace EduardTeodor.Api.Services;

public class PostService(IPostRepository postRepository) : IPostService
{
    public async Task<IEnumerable<PublicPostResponse>> GetAllPublishedAsync(PostFilter parameters)
    {
        if (parameters.PostType is < 1 or > 3) throw new ArgumentException("Invalid post type.");

        var posts = await postRepository.GetAllPublishedAsync(parameters);

        return posts.Select(post => new PublicPostResponse(
            post.Slug,
            post.Title,
            post.PostType,
            post.Summary,
            post.Description,
            post.Starred,
            post.TagNames
        ));
    }

    public async Task<PublicPostResponse?> GetPublishedBySlugAsync(string slug)
    {
        var post = await postRepository.GetPublishedPostBySlugAsync(slug);

        return post is null ? null : new PublicPostResponse(
            post.Slug,
            post.Title,
            post.PostType,
            post.Summary,
            post.Description,
            post.Starred,
            post.TagNames
        );
    }
}
