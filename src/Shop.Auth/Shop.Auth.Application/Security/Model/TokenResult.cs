namespace Shop.Auth.Application.Security.Model
{
    public record TokenResult(string RefreshToken, string AccessToken, int ExpireAt);
}
