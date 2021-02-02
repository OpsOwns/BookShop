using System.Threading.Tasks;
using Shop.Auth.Services.Security.Model;

namespace Shop.Auth.Services.Security.Jwt.Interfaces
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName, string userRole, string subject);
    }
}
