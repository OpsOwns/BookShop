using Shop.Auth.Application.Security.Model;
using Shop.Shared.ResultResponse;

namespace Shop.Auth.Application.User.Command
{
    public record RefreshTokenCommand (string AccessToken, string RefreshToken) : IRequestResultOf<TokenResult>;
}
