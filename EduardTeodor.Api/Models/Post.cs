namespace EduardTeodor.Api.Models;

public class Post
{
    public Guid Id {get;set;}
    public required string Slug {get;set;}
    public required string Title {get;set;}
    public required int PostType {get;set;}
    public string Summary {get;set;} = "";
    public string Description {get;set;} = "";
    public bool Published {get;set;} = false;
    public bool Starred {get;set;} = false;
    public DateTime CreatedAt {get;set;}
    public DateTime UpdatedAt {get;set;}
    public string[] TagNames {get;set;} = [];
}
