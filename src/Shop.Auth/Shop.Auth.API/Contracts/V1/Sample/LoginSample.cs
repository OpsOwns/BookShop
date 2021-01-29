using Shop.Auth.API.Contracts.V1.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Shop.Auth.API.Contracts.V1.Sample
{
    public class LoginSample : IExamplesProvider<LoginUserRequest>
    {
        public LoginUserRequest GetExamples() =>
            new LoginUserRequest("Json", "Conor35!123");
    }
}
