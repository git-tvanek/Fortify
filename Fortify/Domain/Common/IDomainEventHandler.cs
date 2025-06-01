using Fortify.Domain.Events;

namespace Fortify.Domain.Common;

public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : DomainEventBase
{
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}