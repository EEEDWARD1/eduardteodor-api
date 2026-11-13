using EduardTeodor.Application.Interfaces;
using EduardTeodor.Domain.Entities;

namespace EduardTeodor.Application.UseCases;

public class GetPublishedPost(IPostRepository repository)
{
    public Task<Post?> ExecuteAsync(string slug)
    {
        return repository.GetPublishedPostAsync(slug);
    }
}