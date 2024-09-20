using Microsoft.AspNetCore.Identity;

namespace newsApi.Enteties;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    public int ArticleId { get; set; }
    public Article Article { get; set; }

    public NewsUser Author { get; set; }
    public string AuthorId { get; set; }
}