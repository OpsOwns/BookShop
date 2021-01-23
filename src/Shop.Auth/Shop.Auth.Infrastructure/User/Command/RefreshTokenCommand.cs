using Shop.Auth.Infrastructure.Security.Model;
using Shop.Shared.ResultResponse;

namespace Shop.Auth.Infrastructure.User.Command
{
    public record RefreshTokenCommand (string AccessToken, string RefreshToken) : IRequestResultOf<TokenResult>;
}
