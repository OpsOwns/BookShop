using CSharpFunctionalExtensions;
using MediatR;

namespace Shop.Shared.ResultResponse
{
    public interface IRequestResultOf<T> : IRequest<Result<T>>
    { }
    public interface IRequestResult : IRequest<Result>
    { }
}
