namespace newsApi.Enteties;

public class Comment
{
    public int CommentId { get; set; }
    public string Content { get; set; }
    
    public int ArticleId { get; set; }
    public Article Article { get; set; }
}