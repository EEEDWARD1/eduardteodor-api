using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Repositories;

namespace EduardTeodor.Api.Services;

public class AdminPostService(IAdminPostRepository adminPostRepository) : IAdminPostService
{
    public async Task<IEnumerable<PublicPostResponse>> GetAllPostsAsync(PostFilter filter)
    {
        var posts = await adminPostRepository.GetAllPostsAsync(filter);

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
}