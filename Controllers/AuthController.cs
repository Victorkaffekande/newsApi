using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using newsApi.DTOs;
using newsApi.Services;

namespace newsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var res = await _authService.Login(loginDto.email, loginDto.password);
        if (res == null) return Unauthorized("Not a valid login");
        return Ok(res);
    }
}