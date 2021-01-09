namespace Shop.Shared.Domain.Event
{
    public abstract class DomainEventNotification<TDomainEvent> : IDomainEvent where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; }
        public DomainEventNotification(TDomainEvent domainEvent) => DomainEvent = domainEvent;
    }
}
