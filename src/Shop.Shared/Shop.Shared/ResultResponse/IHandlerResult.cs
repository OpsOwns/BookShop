using CSharpFunctionalExtensions;
using MediatR;

namespace Shop.Shared.ResultResponse
{
    public interface IHandlerResultOf<in TIn, TOut> : IRequestHandler<TIn, Result<TOut>>
        where TIn : IRequestResultOf<TOut>
    {
    }

    public interface IHandlerResult<in TRequest> : IRequestHandler<TRequest, Result>
        where TRequest : IRequest<Result>
    {
    }
}