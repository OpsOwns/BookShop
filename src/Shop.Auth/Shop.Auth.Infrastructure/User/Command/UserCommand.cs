using Shop.Auth.Infrastructure.Security.Model;
using Shop.Shared.ResultResponse;

namespace Shop.Auth.Infrastructure.User.Command
{
    public record UserLoginCommand(string Login, string Password) : IRequestResultOf<TokenResult>;
    public record UserRegisterCommand(string Login, string Password, string Email) : IRequestResultOf<string>;
}
