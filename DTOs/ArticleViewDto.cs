using newsApi.Enteties;

namespace newsApi.DTOs;

public class ArticleViewDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public AuthorDto Autor { get; set; }
    

    public static ArticleViewDto FromEntity(Article Article)
    {
        return new ArticleViewDto()
        {
            CreatedAt = Article.CreatedAt,
            Autor = AuthorDto.FromEntity(Article.Autor),
            Content = Article.Content,
            Id = Article.Id,
            Title = Article.Title,
        };
    }
}