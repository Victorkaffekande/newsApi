using newsApi.Enteties;

namespace newsApi;

public class NewsRepo : INewsRepo
{
    private readonly NewsContext _newsContext;

    public NewsRepo(NewsContext newsContext)
    {
        _newsContext = newsContext;
    }

    public List<Article> GetAllArticles()
    {
        return _newsContext.Articles.ToList();
    }

    public Article? GetSingleArticle(int id)
    {
        return _newsContext.Articles.Find(id);
    }
}