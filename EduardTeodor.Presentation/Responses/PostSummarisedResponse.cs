namespace EduardTeodor.Presentation.Responses;

public record PostSummarisedResponse(
    string Slug,
    string Title,
    int PostType,
    string Summary,
    bool Starred,
    string[] TagNames
);