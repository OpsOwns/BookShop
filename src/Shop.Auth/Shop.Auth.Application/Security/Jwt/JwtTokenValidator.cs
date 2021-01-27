using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shop.Auth.Application.Security.Jwt.Interfaces;

namespace Shop.Auth.Application.Security.Jwt
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
