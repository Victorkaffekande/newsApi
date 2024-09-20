using newsApi.Enteties;

namespace newsApi.DTOs;

public class CommentViewDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public AuthorDto Author { get; set; }
    public int ArticleId { get; set; }

    public static CommentViewDto FromEntity(Comment comment)
    {
        return new CommentViewDto()
        {
            Id = comment.Id,
            ArticleId = comment.ArticleId,
            Author = AuthorDto.FromEntity(comment.Author),
            Content = comment.Content
        };
    }
    
}