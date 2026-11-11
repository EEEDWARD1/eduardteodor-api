using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Models;

namespace EduardTeodor.Api.Repositories;
public interface IAdminPostRepository
{
    Task<IEnumerable<Post>> GetAllPostsAsync(PostFilter parameters);
    Task<Post?> CreatePostAsync(CreatePostRequest request);
    Task<bool> DeletePostAsync(string slug);
}
