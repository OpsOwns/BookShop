using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Shop.Auth.Application.Domain;
using Shop.Auth.Application.Security.Jwt.Interfaces;
using Shop.Auth.Application.User.Command;
using Shop.Shared.ResultResponse;

namespace Shop.Auth.Application.User.Handler
{
    public class UserRegisterHandler : IHandlerResultOf<UserRegisterCommand, string>
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly ITokenFactory _tokenFactory;
        public UserRegisterHandler(UserManager<ShopUser> userManager, ITokenFactory tokenFactory)
        {
            _userManager = userManager;
            _tokenFactory = tokenFactory;
        }

        public async Task<Result<string>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ShopUser { UserName = request.Login, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return Result.Failure<string>(JsonConvert.SerializeObject(result.Errors, Formatting.None));
            var userFromDb = await _userManager.FindByNameAsync(request.Login);
            await _userManager.AddToRoleAsync(userFromDb, Roles.USER);
            var refreshToken = _tokenFactory.GenerateToken();
            await _userManager.RemoveAuthenticationTokenAsync(user, "Shop", "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(userFromDb, "Shop", "RefreshToken", refreshToken);
            return Result.Success("User created");
        }
    }
}
