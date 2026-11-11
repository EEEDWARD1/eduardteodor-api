namespace EduardTeodor.Api.DTOs.Posts;

public record PostFilter(
    int? PostType,
    bool? Starred
);