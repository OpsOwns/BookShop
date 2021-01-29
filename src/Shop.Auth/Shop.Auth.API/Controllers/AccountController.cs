using Microsoft.AspNetCore.Mvc;
using Shop.Auth.API.Contracts.V1;
using Shop.Auth.API.Contracts.V1.Request;
using Shop.Shared.API;
using System.Threading.Tasks;
using Shop.Auth.Application.User.Command;

namespace Shop.Auth.API.Controllers
{
    [Route(Routes.Account)]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AccountController : BaseController
    {
        [HttpPost, Route(Routes.Register)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest registerUserRequest)
        {
            var result = await Mediator.Send(new UserRegisterCommand(registerUserRequest.Login, registerUserRequest.Password,
                registerUserRequest.Email));
            return result.IsFailure ? Conflict(result.Error) : Ok(result.Value);
        }
    }
}
