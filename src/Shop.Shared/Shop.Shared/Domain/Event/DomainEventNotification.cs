namespace Shop.Shared.Domain.Event
{
    public abstract class DomainEventNotification<TDomainEvent> : IDomainEvent where TDomainEvent : IDomainEvent
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}