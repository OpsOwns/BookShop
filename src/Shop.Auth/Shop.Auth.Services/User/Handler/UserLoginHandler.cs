using CSharpFunctionalExtensions;
using Dawn;
using Microsoft.AspNetCore.Identity;
using Shop.Auth.Services.Domain;
using Shop.Auth.Services.Security.Jwt;
using Shop.Auth.Services.Security.Jwt.Interfaces;
using Shop.Auth.Services.Security.Model;
using Shop.Auth.Services.User.Command;
using Shop.Shared.ResultResponse;
using Shop.Shared.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Auth.Services.User.Handler
{
    public class UserLoginHandler : ICommandHandler<UserLoginCommand, TokenResult>
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
            Guard.Argument(request.Login, nameof(request.Login)).NotNull();
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
            var jwtToken = await _jwtFactory.GenerateEncodedToken(userFromDb.Id.ToString(), userFromDb.UserName, role[0], userFromDb.Email);
            var refreshToken = _tokenFactory.GenerateToken();
            await _userManager.RemoveAuthenticationTokenAsync(userFromDb, TokenProviderNames.LoginProvider, TokenProviderNames.TokenName);
            await _userManager.SetAuthenticationTokenAsync(userFromDb, TokenProviderNames.LoginProvider,
                TokenProviderNames.TokenName, refreshToken);
            return Result.Success(new TokenResult(refreshToken, jwtToken.Token, jwtToken.ExpiresIn));
        }
    }
}
