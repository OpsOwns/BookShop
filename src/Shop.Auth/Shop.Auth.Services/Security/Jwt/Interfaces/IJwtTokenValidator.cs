using System.Security.Claims;

namespace Shop.Auth.Services.Security.Jwt.Interfaces
{
    public interface IJwtTokenValidator
    {
        ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey);
    }
}
