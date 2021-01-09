namespace Shop.Auth.API.Contracts.V1.Request
{
    public record RegisterUserRequest(string Login, string Password, string Email);
}
