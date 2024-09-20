using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;

namespace newsApi.Enteties;

public class NewsUser : IdentityUser
{
    public List<Article> Articles { get; set; }
    public List<Comment> Comments { get; set; }
}