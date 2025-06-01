using Fortify.Domain.Events;

namespace Fortify.Domain.Common;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(DomainEventBase domainEvent, CancellationToken cancellationToken = default);
    Task DispatchAsync(IEnumerable<DomainEventBase> domainEvents, CancellationToken cancellationToken = default);
}