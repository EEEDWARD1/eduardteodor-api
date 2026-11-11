using EduardTeodor.Api.DTOs.Posts;

namespace EduardTeodor.Api.Services;

public interface IPostService
{
    Task<IEnumerable<PublicPostResponse>> GetAllPublishedAsync(PostFilter parameters);
    Task<PublicPostResponse?> GetPublishedBySlugAsync(string slug);
}
