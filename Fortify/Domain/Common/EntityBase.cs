using Fortify.Domain.Events;

namespace Fortify.Domain.Common;

public abstract class EntityBase : IHasDomainEvents
{
    private readonly List<DomainEventBase> _domainEvents = [];

    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEventBase domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEventBase domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}