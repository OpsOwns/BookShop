using System.Threading;
using System.Threading.Tasks;

namespace Shop.Shared.Domain
{
    public interface IRepository
    {
        Task<int> Save(CancellationToken cancellationToken = default);
    }
}