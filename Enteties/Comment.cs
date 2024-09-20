using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace newsApi.Enteties;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    public int ArticleId { get; set; }
    [JsonIgnore]
    public Article Article { get; set; }

    public NewsUser Author { get; set; }
    public string AuthorId { get; set; }
}