using Shop.Shared.Domain.Event;
using System.Collections.Generic;

namespace Shop.Shared.Domain
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        protected void AddDomainEvent(IDomainEvent newEvent) => _domainEvents.Add(newEvent);
        protected void ClearDomainEvents() => _domainEvents.Clear();
        public static bool operator ==(Entity value1, Entity value2) => value1 is null && value2 is null ||
                   value1 is not null && value2 is not null && value1.Equals(value2);
        public override bool Equals(object obj) => obj is Entity other && (ReferenceEquals(this, other) || GetType() == other.GetType());
        public static bool operator !=(Entity value1, Entity value2) => !(value1 == value2);
        public override int GetHashCode() => _domainEvents.GetHashCode();
    }
}