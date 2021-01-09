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
            var comand = new UserRegisterCommand(registerUserRequest.Login, registerUserRequest.Password,
                registerUserRequest.Email);
            return Ok(await  Mediator.Send(comand));
        }
    }
}
