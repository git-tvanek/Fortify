using Domain.Enums;
using Fortify.Domain.Common;
using Fortify.Domain.Entities;
using Fortify.Domain.Enums;
using Fortify.Domain.Events;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser, IHasDomainEvents
{
    private readonly List<DomainEventBase> _domainEvents = [];

    public AccountStatus AccountStatus { get; set; } = AccountStatus.Active;
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public bool Is2FAEnabled { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public virtual ICollection<ApplicationRole> Roles { get; set; } = new HashSet<ApplicationRole>();

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