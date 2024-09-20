using newsApi.Enteties;

namespace newsApi.DTOs;

public class AuthorDto
{
    public string Id { get; set; }
    public string Username { get; set; }

    public static AuthorDto FromEntity(NewsUser entity)
    {
        return new AuthorDto { Id = entity.Id,
            Username = entity.UserName };
    }
}