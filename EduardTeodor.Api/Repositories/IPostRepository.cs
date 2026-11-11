using EduardTeodor.Api.DTOs.Posts;
using EduardTeodor.Api.Models;

namespace EduardTeodor.Api.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllPublishedAsync(PostFilter parameters);
    Task<Post?> GetPublishedPostBySlugAsync(string slug);
}
