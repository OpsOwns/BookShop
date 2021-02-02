using Microsoft.AspNetCore.Mvc;
using Shop.Auth.API.Contracts.V1;
using Shop.Auth.API.Contracts.V1.Request;
using Shop.Auth.Services.User.Command;
using Shop.Shared.API;
using System.Threading.Tasks;

namespace Shop.Auth.API.Controllers
{
    [Route(Routes.Auth)]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthController : BaseController
    {
        [HttpPut, Route(Routes.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var result = await Mediator.Send(new RefreshTokenCommand(refreshTokenRequest.AccessToken, refreshTokenRequest.RefreshToken));
            return result.IsFailure ? Conflict(result.Error) : Ok(result.Value);
        }


        [HttpPost, Route(Routes.Login)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest loginRequest)
        {
            var result = await Mediator.Send(new UserLoginCommand(loginRequest.Login, loginRequest.Password));
            return result.IsFailure ? Conflict(result.Error) : Ok(result.Value);
        }
    }
}
