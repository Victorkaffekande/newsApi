using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using newsApi.DTOs;
using newsApi.Enteties;

namespace newsApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CommentController : Controller
{
    private INewsRepo _newsRepo;
    private readonly NewsContext _newsContext;

    public CommentController(INewsRepo newsRepo, NewsContext newsContext)
    {
        _newsRepo = newsRepo;
        _newsContext = newsContext;
    }

    [HttpPost]
    [Route("createComment")]
    [Authorize(Policy = "subscriberPolicy")]
    public IActionResult CreateComment(CommentCreateDto commentCreateDto)
    {
        var userId = HttpContext.User.Claims.First(c => c.Type == "id").Value;

        //check if article exists
        var article = _newsContext.Articles.Find(commentCreateDto.articleId);
        if (article == null) return BadRequest("Article does not exist");
        var newsUser = _newsContext.Users.Find(userId)!;
        var comment = new Comment()
        {
            AuthorId = userId,
            Content = commentCreateDto.content,
            ArticleId = commentCreateDto.articleId,
            Author = newsUser
        };
        _newsContext.Comments.Add(comment);
        _newsContext.SaveChanges();
        return Ok(CommentViewDto.FromEntity(comment));
    }

    [HttpPut]
    [Route("updateComment")]
    [Authorize(Policy = "editorPolicy")]
    public IActionResult UpdateComment(CommentUpdateDto commentUpdateDto)
    {
        var comment = _newsContext.Comments.Include(x => x.Author).First(x => x.Id == commentUpdateDto.CommentId);
        if (comment == null) return BadRequest("comment does not exist");
        comment.Content = commentUpdateDto.Content;
        _newsContext.SaveChanges();
        
        return Ok(CommentViewDto.FromEntity(comment));
    }


    [HttpDelete]
    [Route("deleteComment/{id}")]
    [Authorize(Policy = "editorPolicy")]
    public IActionResult UpdateComment([FromRoute]int id)
    {
        var comment = _newsContext.Comments.Find(id);
        if (comment == null) return BadRequest("comment does not exist");

        _newsContext.Comments.Remove(comment);
        _newsContext.SaveChanges();
        return Ok();
    }
}