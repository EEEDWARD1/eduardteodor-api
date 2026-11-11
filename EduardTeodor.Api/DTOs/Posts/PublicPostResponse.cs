namespace EduardTeodor.Api.DTOs.Posts;

public record PublicPostResponse(
    string Slug,
    string Title,
    int PostType,
    string Summary,
    string Description,
    bool Starred,
    string[] TagNames
);