using EduardTeodor.Api.DTOs.Posts;

namespace EduardTeodor.Api.Services;

public interface IPostService
{
    Task<IEnumerable<PublicPostResponse>> GetAllPublishedAsync();
    Task<PublicPostResponse?> GetPublishedBySlugAsync(string slug);
}
