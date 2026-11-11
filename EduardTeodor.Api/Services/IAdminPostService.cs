using EduardTeodor.Api.DTOs.Posts;

namespace EduardTeodor.Api.Services;

public interface IAdminPostService
{
    Task<IEnumerable<PublicPostResponse>> GetAllPostsAsync(PostFilter filter);
}