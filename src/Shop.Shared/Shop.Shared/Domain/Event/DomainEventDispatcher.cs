using MediatR;
using System;
using System.Threading.Tasks;

namespace Shop.Shared.Domain.Event
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        public DomainEventDispatcher(IMediator mediator) => _mediator = mediator;
        public async Task Dispatch(params IDomainEvent[] events)
        {
            foreach (var @event in events)
            {
                await _mediator.Publish(_createDomainEventNotification(@event)).ConfigureAwait(false);
            }
        }
        private IDomainEvent _createDomainEventNotification(IDomainEvent domainEvent) => (IDomainEvent)Activator.CreateInstance(typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
    }
}
