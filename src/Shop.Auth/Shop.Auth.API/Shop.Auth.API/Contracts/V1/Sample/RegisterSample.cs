using Shop.Auth.API.Contracts.V1.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Shop.Auth.API.Contracts.V1.Sample
{
    public class RegisterSample : IExamplesProvider<RegisterUserRequest>
    {
        public RegisterUserRequest GetExamples() =>
            new RegisterUserRequest("Json", "Conor35!123", "json.conor@gmail.com");
    }
}
