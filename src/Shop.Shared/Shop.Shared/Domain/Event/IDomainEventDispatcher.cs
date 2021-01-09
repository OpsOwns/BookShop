using System.Threading.Tasks;

namespace Shop.Shared.Domain.Event
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(params IDomainEvent[] events);
    }
}