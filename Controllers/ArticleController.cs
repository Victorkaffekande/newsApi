using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newsApi.DTOs;
using newsApi.Enteties;

namespace newsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : Controller
{
    private INewsRepo _newsRepo;
    private readonly NewsContext _newsContext;

    public ArticleController(INewsRepo newsRepo, NewsContext newsContext)
    {
        _newsRepo = newsRepo;
        _newsContext = newsContext;
    }

    [HttpGet]
    [Route("getAllArticles")]
    [AllowAnonymous]
    public IActionResult GetAllArticles()
    {
        return Ok(_newsRepo.GetAllArticles());
    }

    [HttpGet]
    [Route("getArticle/{id}")]
    [Authorize]
    [Authorize(Policy = "subscriberPolicy")]
    public IActionResult GetSingleArticle(int id)
    {
        var article = _newsRepo.GetSingleArticle(id);
        if (article != null)
        {
            return Ok(article);
        }

        return NotFound("That article does not exist");
    }
    

    [HttpPut]
    [Route("updateArticle")]
    [Authorize(Policy = "editorOrOwnerPolicy")]
    public IActionResult UpdateArticle(ArticleUpdateDto articleUpdateDto)
    {
        var article = _newsContext.Articles.Include(x => x.Autor).First(x => x.Id == articleUpdateDto.Id);

        article.Title = articleUpdateDto.Title;
        article.Content = articleUpdateDto.Content;
        _newsContext.SaveChanges();
        return Ok(ArticleViewDto.FromEntity(article));
    }
    
    //todo delete article | is editor
    [HttpDelete]
    [Route("deleteArticle/{id}")]
    [Authorize(Policy = "editorPolicy")]
    public IActionResult UpdateArticle([FromRoute] int id)
    {
        var article = _newsContext.Articles.Find(id);
        if (article == null) return NotFound("no article found");
        _newsContext.Remove(article);
        _newsContext.SaveChanges();
        return Ok();
    }
    
    [HttpPost]
    [Route("createArticle")]
    [Authorize(Policy = "writerPolicy")]
    public IActionResult CreateArticle(ArticleCreateDto articleCreateDto)
    {
        var userId = HttpContext.User.Claims.First(c => c.Type == "id").Value;
        var author = _newsContext.Users.Find(userId);
        var newArticle = new Article()
        {
            AuthorId = userId,
            CreatedAt = DateTime.Now,
            Title = articleCreateDto.Title,
            Content = articleCreateDto.Content,
            Autor = author
        };
        _newsContext.Articles.Add(newArticle);
        _newsContext.SaveChanges();
        return Ok(ArticleViewDto.FromEntity(newArticle));
    }
}