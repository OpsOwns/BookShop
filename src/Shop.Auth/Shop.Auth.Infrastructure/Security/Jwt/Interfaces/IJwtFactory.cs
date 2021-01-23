using Shop.Auth.Infrastructure.Security.Model;
using System.Threading.Tasks;

namespace Shop.Auth.Infrastructure.Security.Jwt.Interfaces
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName, string userRole, string subject);
    }
}
