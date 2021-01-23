using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Shop.Auth.Infrastructure.Security.Jwt;
using Shop.Auth.Infrastructure.Security.Jwt.Interfaces;
using Shop.Auth.Infrastructure.Security.Model;
using Shop.Auth.Infrastructure.User.Model;
using Shop.Shared.ResultResponse;
using Shop.Shared.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Auth.Infrastructure.User.Command
{
    public class UserLoginHandler : IHandlerResultOf<UserLoginCommand, TokenResult>
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly ITokenFactory _tokenFactory;
        public UserLoginHandler(UserManager<ShopUser> userManager, ITokenFactory tokenFactory, IJwtFactory jwtFactory)
        {
            _userManager = userManager;
            _tokenFactory = tokenFactory;
            _jwtFactory = jwtFactory;
        }

        public async Task<Result<TokenResult>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            if (request.Login.IsEmpty())
                throw new AuthException($"invalid {request.Login}");
            if (request.Password.IsEmpty())
                throw new AuthException($"invalid {request.Password}");
            var userFromDb = await _userManager.FindByNameAsync(request.Login);
            if (userFromDb == null)
                return Result.Failure<TokenResult>($"User {request.Login} not exists");
            var result = await _userManager.CheckPasswordAsync(userFromDb, request.Password);
            if (!result)
                return Result.Failure<TokenResult>("Invalid password!");
            var role = await _userManager.GetRolesAsync(userFromDb);
            var jwtToken = await _jwtFactory.GenerateEncodedToken(userFromDb.Id.ToString(), userFromDb.UserName, role[0]);
            var refreshToken = _tokenFactory.GenerateToken();
            await _userManager.RemoveAuthenticationTokenAsync(userFromDb, TokenProviderNames.LOGIN_PROVIDER, TokenProviderNames.TOKEN_NAME);
            await _userManager.SetAuthenticationTokenAsync(userFromDb, TokenProviderNames.LOGIN_PROVIDER,
                TokenProviderNames.TOKEN_NAME, refreshToken);
            return Result.Success(new TokenResult(refreshToken, jwtToken.Token, jwtToken.ExpiresIn));
        }
    }
}
