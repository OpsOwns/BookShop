using CSharpFunctionalExtensions;
using MediatR;

namespace Shop.Shared.ResultResponse
{
    public interface ICommandResultOf<T> : IRequest<Result<T>>
    {
    }

    public interface ICommandResult : IRequest<Result>
    {
    }
}