using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Models;

namespace EduardTeodor.Api.Services;

public interface IAdminPostService
{
    Task<IEnumerable<PublicPostResponse>> GetAllPostsAsync(PostFilter filter);
    Task<Post?> CreatePostAsync(CreatePostRequest request);
    Task<bool> DeletePostAsync(string slug);
}
