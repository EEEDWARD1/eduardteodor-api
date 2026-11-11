using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Models;

namespace EduardTeodor.Api.Repositories;
public interface IAdminPostRepository
{
    Task<IEnumerable<Post>> GetAllPostsAsync(PostFilter parameters);
}
