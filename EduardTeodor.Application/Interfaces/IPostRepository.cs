using EduardTeodor.Domain.Entities;

namespace EduardTeodor.Application.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllPublishedPostsAsync(int? postType, bool? starred);
    Task<Post?> GetPublishedPostAsync(string slug);
}