using CSharpFunctionalExtensions;
using MediatR;

namespace Shop.Shared.ResultResponse
{
    public interface IQueryResult<T> : IRequest<Result<T>>
    {
    }
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
