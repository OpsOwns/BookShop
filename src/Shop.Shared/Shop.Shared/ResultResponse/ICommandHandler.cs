using CSharpFunctionalExtensions;
using MediatR;

namespace Shop.Shared.ResultResponse
{
    public interface ICommandHandler<in TIn, TOut> : IRequestHandler<TIn, Result<TOut>>
        where TIn : ICommandResultOf<TOut>
    {
    }

    public interface ICommandHandlerResult<in TRequest> : IRequestHandler<TRequest, Result>
        where TRequest : IRequest<Result>
    {
    }
}