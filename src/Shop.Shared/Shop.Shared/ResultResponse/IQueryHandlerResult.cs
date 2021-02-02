using CSharpFunctionalExtensions;
using MediatR;

namespace Shop.Shared.ResultResponse
{
    public interface IQueryHandlerResult<in TIn, TOut> : IRequestHandler<TIn, Result<TOut>>
        where TIn : IQueryResult<TOut>
    {
    }
    public interface IQueryHandler<in TIn, TOut> : IRequestHandler<TIn, TOut>
        where TIn : IQuery<TOut>
    {
    }
}
