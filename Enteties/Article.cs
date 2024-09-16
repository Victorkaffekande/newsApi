using Microsoft.AspNetCore.Identity;

namespace newsApi.Enteties;

public class Article
{
    public int ArticleId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public int AuthorId { get; set; }
    public IdentityUser Autor { get; set; }
}