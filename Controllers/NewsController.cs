using Microsoft.AspNetCore.Mvc;

namespace newsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsController : Controller
{
    [HttpGet]
    public IActionResult filler()
    {
        return Ok("filler");
    }
}
