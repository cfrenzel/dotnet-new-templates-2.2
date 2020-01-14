using System;
using System.Collections.Concurrent;

namespace SolutionName.Core
{
    public interface IEntity
    {
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
    }
}
