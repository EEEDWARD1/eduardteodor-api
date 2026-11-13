namespace EduardTeodor.Presentation.Responses;

public record PostResponse(
    string Slug,
    string Title,
    int PostType,
    string Summary,
    string Description,
    bool Starred,
    string[] TagNames
);