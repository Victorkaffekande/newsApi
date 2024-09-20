using newsApi.Enteties;

namespace newsApi;

public interface INewsRepo
{
    List<Article> GetAllArticles();
    Article? GetSingleArticle(int id);
}