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
        [HttpPut, Route(Routes.REFRESH)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var result = await Mediator.Send(new RefreshTokenCommand(refreshTokenRequest.AccessToken, refreshTokenRequest.RefreshToken));
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
