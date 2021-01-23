using System.Collections.Generic;
using System.Diagnostics;
using Dawn;
using Shop.Shared.Domain.Event;
using EntityFx = CSharpFunctionalExtensions.Entity<int>;
namespace Shop.Shared.Domain
{
    public class Entity : EntityFx
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        protected void AddDomainEvent(IDomainEvent newEvent) => _domainEvents.Add(newEvent);
        protected void ClearDomainEvents() => _domainEvents.Clear();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override int Id { get; protected set; }
        protected Entity() { }
        public void SetIdentity(BaseId id)
        {
            Guard.Argument(id.Value, nameof(id)).NotZero().NotNegative();
            Id = id.Value;
        }
    }
}
