using System;
using MediatR;

namespace CR_COMPUTER.Domain.Events
{
    /// <summary>
    /// Base class for domain events
    /// </summary>
    public abstract class DomainEvent : INotification
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
