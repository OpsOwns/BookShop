using MediatR;

namespace Shop.Shared.Domain.Event
{
    public interface IDomainEventHandler<T> : INotificationHandler<DomainEventNotification<T>> where T : IDomainEvent
    {
    }
}
