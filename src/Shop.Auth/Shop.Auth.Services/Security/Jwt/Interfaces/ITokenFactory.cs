namespace Shop.Auth.Services.Security.Jwt.Interfaces
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}