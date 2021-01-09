using MediatR;

namespace Shop.Auth.Infrastructure.User.Command
{
    public record UserRegisterCommand(string Login, string Password, string Email) : IRequest<bool>;
}
