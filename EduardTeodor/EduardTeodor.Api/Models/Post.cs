namespace EduardTeodor.Api.Models;

public class Post
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public PostType PostType { get; set; } = PostType.Thought;
    public required string Slug { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Image { get; set; }
    public required string Content { get; set; }
    public string? Href { get; set; }
    public List<Tag> Tags { get; set; } = [];

}