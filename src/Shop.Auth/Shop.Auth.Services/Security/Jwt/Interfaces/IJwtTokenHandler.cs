using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Shop.Auth.Services.Security.Jwt.Interfaces
{
    public interface IJwtTokenHandler
    {
        string WriteToken(JwtSecurityToken jwt);
        ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters);
    }
}
