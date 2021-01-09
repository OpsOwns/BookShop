using FluentValidation;
using MediatR;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Shared.Shared
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators.Select(fail => fail.Validate(context)).
                SelectMany(errors => errors.Errors).Where(notNull => notNull != null).ToList();
            if (failures.Count <= 0) return await next();
            {
                failures.ForEach(errors => Log.Error(errors.ErrorMessage));
                throw new ValidationException(failures);
            }
        }
    }
}
