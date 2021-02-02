using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Shop.Auth.Services.Domain;
using Shop.Auth.Services.Security.Jwt;
using Shop.Auth.Services.Security.Jwt.Interfaces;
using Shop.Auth.Services.Security.Model;
using Shop.Auth.Services.User.Command;
using Shop.Shared.ResultResponse;
using Shop.Shared.Shared;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Auth.Services.User.Handler
{
    public class RefreshTokenHandler : ICommandHandler<RefreshTokenCommand, TokenResult>
    {
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly IJwtFactory _jwtFactory;
        private readonly ITokenFactory _tokenFactory;
        private readonly UserManager<ShopUser> _userManager;
        private readonly AuthSettings _authSettings;
        public RefreshTokenHandler(IJwtTokenValidator jwtTokenValidator, IJwtFactory jwtFactory, ITokenFactory tokenFactory, UserManager<ShopUser> userManager, AuthSettings authSettings)
        {
            _jwtTokenValidator = jwtTokenValidator;
            _jwtFactory = jwtFactory;
            _tokenFactory = tokenFactory;
            _userManager = userManager;
            _authSettings = authSettings;
        }

        public async Task<Result<TokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request.AccessToken.IsEmpty())
                throw new AuthException($"invalid {nameof(request.AccessToken)}");
            if (request.RefreshToken.IsEmpty())
                throw new AuthException($"invalid {nameof(request.RefreshToken)}");
            var claimsPrincipal = _jwtTokenValidator.GetPrincipalFromToken(request.AccessToken, _authSettings.SecretKey);
            if (claimsPrincipal is null)
                return Result.Failure<TokenResult>("Invalid access token");
            var id = claimsPrincipal.Claims.First(c => c.Type == ClaimTypes.Sid);
            var role = claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.Role);
            var user = await _userManager.FindByIdAsync(id.Value);
            var jwtToken = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.UserName, role.Value, user.Email);
            var refreshToken = _tokenFactory.GenerateToken();
            await _userManager.RemoveAuthenticationTokenAsync(user, TokenProviderNames.LoginProvider,
                  TokenProviderNames.TokenName);
            await _userManager.SetAuthenticationTokenAsync(user, TokenProviderNames.LoginProvider,
                TokenProviderNames.TokenName, refreshToken);
            return Result.Success(new TokenResult(refreshToken, jwtToken.Token, jwtToken.ExpiresIn));
        }
    }
}
