using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Auth.Services.Security.Jwt;

namespace Shop.Auth.API.Issues
{
    public class AuthProblemDetails : ProblemDetails
    {
        public AuthProblemDetails(AuthException authException)
        {
            Title = "Code Error";
            Status = StatusCodes.Status409Conflict;
            Detail = authException.Code;
            Type = "Unexpected error";
        }
    }
}
