using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Shop.Auth.Infrastructure.User.Model;
using Shop.Shared.ResultResponse;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Auth.Infrastructure.User.Command
{
    public class UserLoginHandler : IHandlerResult<UserLoginCommand>
    {
        private readonly SignInManager<ShopUser> _signInManager;
        private readonly UserManager<ShopUser> _userManager;
        public UserLoginHandler(SignInManager<ShopUser> signInManager, UserManager<ShopUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<Result> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _userManager.FindByNameAsync(request.Login);
            if (userFromDb == null)
                return Result.Failure($"User {request.Login} not exists");
            var result = await _signInManager.CheckPasswordSignInAsync(userFromDb, request.Password, false);
            return !result.Succeeded ? Result.Failure("Invalid password") : Result.Success("Works");
        }
    }
}
