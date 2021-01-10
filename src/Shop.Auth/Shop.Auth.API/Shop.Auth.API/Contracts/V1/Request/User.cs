namespace Shop.Auth.API.Contracts.V1.Request
{
    public record RegisterUserRequest(string Login, string Password, string Email);

    public record LoginUserRequest(string Login, string Password);
}
