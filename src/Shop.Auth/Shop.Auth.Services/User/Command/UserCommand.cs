using Shop.Auth.Services.Security.Model;
using Shop.Shared.ResultResponse;

namespace Shop.Auth.Services.User.Command
{
    public record UserLoginCommand(string Login, string Password) : ICommand<TokenResult>;
    public record UserRegisterCommand(string Login, string Password, string Email) : ICommand<string>;
}
