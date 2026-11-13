using EduardTeodor.Application.Interfaces;
using EduardTeodor.Domain.Entities;

namespace EduardTeodor.Application.UseCases;

public class GetAllPublishedPosts(IPostRepository repository)
{
    public Task<IEnumerable<Post>> ExecuteAsync(int? postType, bool? starred)
    {
        return repository.GetAllPublishedPostsAsync(postType, starred);
    }
}