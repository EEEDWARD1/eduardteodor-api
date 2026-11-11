using EduardTeodor.Api.Models;

namespace EduardTeodor.Api.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllPublishedAsync();
    Task<Post?> GetPublishedPostBySlugAsync(string slug);
}
