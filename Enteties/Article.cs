using Microsoft.AspNetCore.Identity;

namespace newsApi.Enteties;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public string AuthorId { get; set; }
    public NewsUser Autor { get; set; }

    public List<Comment> Comments { get; set; }
}