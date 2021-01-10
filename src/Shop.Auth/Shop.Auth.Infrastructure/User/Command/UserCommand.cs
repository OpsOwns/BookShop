using Shop.Shared.ResultResponse;

namespace Shop.Auth.Infrastructure.User.Command
{
    public record UserLoginCommand(string Login, string Password) : IRequestResult;
    public record UserRegisterCommand(string Login, string Password, string Email) : IRequestResultOf<string>;
}
