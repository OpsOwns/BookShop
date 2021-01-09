using System;

namespace Shop.Shared.Domain.Event
{
    public abstract class BaseEvent
    {
        public DateTime OccurredAt { get; } = DateTime.Now;
    }
}
