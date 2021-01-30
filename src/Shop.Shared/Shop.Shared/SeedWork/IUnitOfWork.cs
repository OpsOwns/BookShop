using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Shared.SeedWork
{
    public interface IUnitOfWork
    {
        public Task<Result> Commit(CancellationToken token = default);
    }
}
