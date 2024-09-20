using System.IdentityModel.Tokens.Jwt;
using newsApi.DTOs;

namespace newsApi.Services;

public interface IAuthService
{
    Task<string?> Login(string email,string password);
}