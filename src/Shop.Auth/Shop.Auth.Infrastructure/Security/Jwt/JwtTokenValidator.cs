using Microsoft.IdentityModel.Tokens;
using Shop.Auth.Infrastructure.Security.Jwt.Interfaces;
using System.Security.Claims;
using System.Text;

namespace Shop.Auth.Infrastructure.Security.Jwt
{
    public class JwtTokenValidator : IJwtTokenValidator
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        public JwtTokenValidator(IJwtTokenHandler jwtTokenHandler) => _jwtTokenHandler = jwtTokenHandler;
        public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey) =>
            _jwtTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ValidateLifetime = false
            });
    }
}
