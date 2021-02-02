using Shop.Auth.Services.Security.Model;
using Shop.Shared.ResultResponse;

namespace Shop.Auth.Services.User.Command
{
    public record RefreshTokenCommand (string AccessToken, string RefreshToken) : ICommand<TokenResult>;
}
