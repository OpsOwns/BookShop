using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shop.Auth.Infrastructure.Security.Jwt.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shop.Auth.Infrastructure.Security.Jwt
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        public JwtTokenHandler() => _jwtSecurityTokenHandler ??= new JwtSecurityTokenHandler();
        public string WriteToken(JwtSecurityToken jwt) => _jwtSecurityTokenHandler.WriteToken(jwt);
        public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
                return securityToken is not JwtSecurityToken jwtSecurityToken ||
                       !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                           StringComparison.InvariantCultureIgnoreCase)
                    ? throw new SecurityTokenException("Invalid token")
                    : principal;
            }
            catch (Exception ex)
            {
                Log.Error($"Token validation failed: {ex.Message}");
                throw;
            }
        }
    }
}
