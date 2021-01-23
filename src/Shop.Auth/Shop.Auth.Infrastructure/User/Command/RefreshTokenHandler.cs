using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Shop.Auth.Infrastructure.Security.Jwt;
using Shop.Auth.Infrastructure.Security.Jwt.Interfaces;
using Shop.Auth.Infrastructure.Security.Model;
using Shop.Auth.Infrastructure.User.Model;
using Shop.Shared.ResultResponse;
using Shop.Shared.Shared;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Auth.Infrastructure.User.Command
{
    public class RefreshTokenHandler : IHandlerResultOf<RefreshTokenCommand, TokenResult>
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
            var jwtToken = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.UserName, role.Value);
            var refreshToken = _tokenFactory.GenerateToken();
            await _userManager.RemoveAuthenticationTokenAsync(user, TokenProviderNames.LOGIN_PROVIDER,
                  TokenProviderNames.TOKEN_NAME);
            await _userManager.SetAuthenticationTokenAsync(user, TokenProviderNames.LOGIN_PROVIDER,
                TokenProviderNames.TOKEN_NAME, refreshToken);
            return Result.Success(new TokenResult(refreshToken, jwtToken.Token, jwtToken.ExpiresIn));
        }
    }
}
