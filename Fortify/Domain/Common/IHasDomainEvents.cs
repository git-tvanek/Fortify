using Fortify.Domain.Events;

namespace Fortify.Domain.Common;

public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
    void AddDomainEvent(DomainEventBase domainEvent);
    void RemoveDomainEvent(DomainEventBase domainEvent);
    void ClearDomainEvents();
}