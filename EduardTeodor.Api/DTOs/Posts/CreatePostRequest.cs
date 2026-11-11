using System.ComponentModel.DataAnnotations;

namespace EduardTeodor.Api.DTOs.Posts;

public record CreatePostRequest
{
    [Required, MaxLength(255)]
    public string Slug { get; init; } = "";

    [Required, MaxLength(255)]
    public string Title { get; init; } = "";

    [Range(1, 3)]
    public int PostType { get; init; } = 1;

    [Required(AllowEmptyStrings = true)]
    public string Summary { get; init; } = "";

    [Required(AllowEmptyStrings = true)]
    public string Description { get; init; } = "";

    public bool Published { get; init; }
    public bool Starred { get; init; }

    [Required]
    public string[] TagNames { get; init; } = [];
}
