namespace newsApi.DTOs;

public class CommentCreateDto
{
    public string content  { get; set; }
    public int articleId { get; set; }
}