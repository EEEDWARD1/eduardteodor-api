using EduardTeodor.Api.Models;

namespace EduardTeodor.Api.Contracts;

public record PostResponse(
    Guid Id,
    PostType PostType,
    string Slug,
    string Title,
    string Description,
    string? Image,
    string Content,
    string? Href,
    List<string> Tags);
