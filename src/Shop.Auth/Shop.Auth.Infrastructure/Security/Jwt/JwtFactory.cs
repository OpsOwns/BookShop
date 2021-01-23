using Microsoft.Extensions.Options;
using Shop.Auth.Infrastructure.Security.Jwt.Interfaces;
using Shop.Auth.Infrastructure.Security.Model;
using Shop.Shared.Shared;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Auth.Infrastructure.Security.Jwt
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IJwtTokenHandler jwtTokenHandler, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _jwtOptions = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions.Value));
            if (_jwtOptions.ValidFor <= TimeSpan.Zero)
                throw new AuthException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));

            if (_jwtOptions.SigningCredentials == null)
                throw new AuthException(nameof(JwtIssuerOptions.SigningCredentials));

            if (_jwtOptions.JtiGenerator == null)
                throw new AuthException(nameof(JwtIssuerOptions.JtiGenerator));
        }

        public async Task<AccessToken> GenerateEncodedToken(string id, string userName, string role, string subject)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, (_jwtOptions.IssuedAt).ToUnixEpochDate().ToString(),
                    ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Sid, id),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Country, RegionInfo.CurrentRegion.DisplayName!),
                new Claim(ClaimTypes.Name, userName)
            };
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.NotBefore,
                _jwtOptions.Expiration,
                _jwtOptions.SigningCredentials);

            return new AccessToken(_jwtTokenHandler.WriteToken(jwt), (int) _jwtOptions.ValidFor.TotalSeconds);
        }
    }
}
