using System;

namespace Shop.Auth.Infrastructure.Security.Model
{
    public record TokenResult(string RefreshToken, string AccessToken, int ExpireAt);
}
