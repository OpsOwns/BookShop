using FluentValidation;
using Shop.Auth.Infrastructure.User.Command;

namespace Shop.Auth.Infrastructure.User.Validate
{
    public class UserRegisterValid : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterValid()
        {
            RuleFor(x => x.Login).NotEmpty().NotNull();
        }
    }
}
