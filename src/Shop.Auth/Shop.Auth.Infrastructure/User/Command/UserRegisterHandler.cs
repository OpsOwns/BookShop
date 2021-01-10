using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Shop.Auth.Infrastructure.User.Common;
using Shop.Auth.Infrastructure.User.Model;
using Shop.Shared.ResultResponse;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Auth.Infrastructure.User.Command
{
    public class UserRegisterHandler : IHandlerResultOf<UserRegisterCommand, string>
    {
        private readonly UserManager<ShopUser> _userManager;
        public UserRegisterHandler(UserManager<ShopUser> userManager) => _userManager = userManager;
        public async Task<Result<string>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ShopUser { UserName = request.Login, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return Result.Failure<string>(JsonConvert.SerializeObject(result.Errors, Formatting.None));
            var userFromDb = await _userManager.FindByNameAsync(request.Login);
            await _userManager.AddToRoleAsync(userFromDb, Roles.USER);
            return Result.Success("User created");
        }
    }
}
