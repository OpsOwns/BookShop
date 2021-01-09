using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Auth.Infrastructure.User.Command
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, bool>
    {
        public async Task<bool> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
