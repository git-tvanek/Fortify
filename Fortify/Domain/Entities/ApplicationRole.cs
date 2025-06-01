using Domain.Entities;
using Fortify.Domain.Common;
using Fortify.Domain.Events;
using Microsoft.AspNetCore.Identity;
using System.Security;

namespace Fortify.Domain.Entities;

public class ApplicationRole : IdentityRole, IHasDomainEvents
{
    private readonly List<DomainEventBase> _domainEvents = [];

    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
    public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();

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