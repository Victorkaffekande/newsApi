using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using newsApi.Enteties;

namespace newsApi.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<NewsUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<NewsUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string?> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user!, password)) return null;

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault();

        await _userManager.AddClaimAsync(user, new Claim("role", role));
        var authClaims = new List<Claim>
        {
            new Claim("email", user.Email!),
        };
        foreach (var userRole in roles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            authClaims.Add(new Claim("id", user.Id));
        }
        
        var token = CreateToken(authClaims);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        _ = int.TryParse(_configuration["JwtSettings:TokenValidityInHours"], out var tokenValidityInHours);
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: DateTime.Now.AddHours(tokenValidityInHours),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
}