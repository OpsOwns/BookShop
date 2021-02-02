using CSharpFunctionalExtensions;
using MediatR;

namespace Shop.Shared.ResultResponse
{
    public interface ICommand<T> : IRequest<Result<T>>
    {
    }

    public interface ICommandResult : IRequest<Result>
    {
    }
}