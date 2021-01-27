namespace Shop.Auth.Application.Security.Jwt.Interfaces
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}