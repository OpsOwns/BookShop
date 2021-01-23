namespace Shop.Auth.API.Contracts.V1.Request
{
    public record RefreshTokenRequest(string RefreshToken, string AccessToken);
}
