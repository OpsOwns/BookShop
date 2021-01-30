using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Shared.Shared
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public ValidationProblemDetails(ValidationException exception)
        {
            Title = "Validation Error";
            Status = StatusCodes.Status409Conflict;
            Detail = exception.Message;
            Type = "Validation";
        }
    }
}