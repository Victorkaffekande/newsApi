using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using newsApi.DTOs;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace newsApi;

public class EditorOrOwnerRequirementAuthorizationHandler : AuthorizationHandler<EditorOrOwnerRequirement>
{
    private readonly NewsContext _newsContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EditorOrOwnerRequirementAuthorizationHandler(NewsContext newsContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _newsContext = newsContext;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        EditorOrOwnerRequirement requirement)
    {
        var request = _httpContextAccessor.HttpContext.Request;
            request.EnableBuffering();
            
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "id");
        var roleClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (userIdClaim == null || roleClaim == null) return Task.CompletedTask;

        var role = roleClaim.Value;
        var userId = userIdClaim.Value;
        if (role == "Editor")
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (role != "Writer") return Task.CompletedTask;

        using var reader = new StreamReader(_httpContextAccessor.HttpContext.Request.Body, leaveOpen:true);
        var body = reader.ReadToEndAsync().Result;
        try
        {
            var articleUpdateDto = JsonSerializer.Deserialize<ArticleUpdateDto>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (articleUpdateDto == null) return Task.CompletedTask;
            
            var article = _newsContext.Articles.Find(articleUpdateDto.Id);
            
            if (article.AuthorId == userId) context.Succeed(requirement);
            request.Body.Position = 0; 
        }
        catch (JsonException ex)
        {
            return Task.CompletedTask;
        }


        return Task.CompletedTask;
    }
}