namespace Shop.Auth.Services.Security.Model
{
    public record TokenResult(string RefreshToken, string AccessToken, int ExpireAt);
}
