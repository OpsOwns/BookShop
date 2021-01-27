using System.Threading.Tasks;
using Shop.Auth.Application.Security.Model;

namespace Shop.Auth.Application.Security.Jwt.Interfaces
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName, string userRole, string subject);
    }
}
