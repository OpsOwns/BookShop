using Microsoft.AspNetCore.Mvc;
using Shop.Auth.API.Contracts.V1;
using Shop.Auth.API.Contracts.V1.Request;
using Shop.Auth.Infrastructure.User.Command;
using Shop.Shared.API;
using System.Threading.Tasks;

namespace Shop.Auth.API.Controllers
{
    [Route(Routes.AUTH)]
    public class AuthController : BaseController
    {
        [HttpPost, Route(Routes.REGISTER)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest registerUserRequest)
        {
            var result = await Mediator.Send(new UserRegisterCommand(registerUserRequest.Login, registerUserRequest.Password,
                registerUserRequest.Email));
            return result.IsFailure ? Conflict(result.Error) : Ok(result.Value);
        }

        [HttpPost, Route(Routes.LOGIN)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest loginRequest)
        {
            var result = await Mediator.Send(new UserLoginCommand(loginRequest.Login, loginRequest.Password));
            return result.IsFailure ? Conflict(result.Error) : Ok(result);
        }
    }
}
