using Fortify.Domain.Common;
using Fortify.Domain.Entities;
using Fortify.Domain.Events;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;

namespace Fortify.Domain.Entities;

public class ApplicationRole : IdentityRole, IHasDomainEvents
{
    private readonly List<DomainEventBase> _domainEvents = [];
    private readonly List<Permission> _permissions = [];
    private readonly List<ApplicationUser> _users = [];

    public string? Description { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime? DateModified { get; private set; }
    public bool IsSystemRole { get; private set; }
    public bool IsActive { get; private set; } = true;

    public virtual IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();
    public virtual IReadOnlyCollection<ApplicationUser> Users => _users.AsReadOnly();
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected ApplicationRole() { }

    public ApplicationRole(string name, string? description = null, bool isSystemRole = false)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        NormalizedName = name.ToUpperInvariant();
        Description = description;
        IsSystemRole = isSystemRole;
        DateCreated = DateTime.UtcNow;
        ConcurrencyStamp = Guid.NewGuid().ToString();

        AddDomainEvent(new RoleCreatedEvent
        {
            RoleId = Id,
            RoleName = Name,
            Description = Description,
            IsSystemRole = IsSystemRole,
            CreatedAt = DateCreated
        });
    }

    public void UpdateDetails(string name, string? description)
    {
        if (IsSystemRole)
            throw new InvalidOperationException("Cannot update system role details");

        Name = name;
        NormalizedName = name.ToUpperInvariant();
        Description = description;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new RoleUpdatedEvent
        {
            RoleId = Id,
            RoleName = Name,
            Description = Description,
            UpdatedAt = DateTime.UtcNow
        });
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new RoleActivatedEvent
        {
            RoleId = Id,
            RoleName = Name!,
            ActivatedAt = DateTime.UtcNow
        });
    }

    public void Deactivate()
    {
        if (!IsActive || IsSystemRole)
            return;

        IsActive = false;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new RoleDeactivatedEvent
        {
            RoleId = Id,
            RoleName = Name!,
            DeactivatedAt = DateTime.UtcNow
        });
    }

    public void AddPermission(Permission permission)
    {
        if (_permissions.Any(p => p.Id == permission.Id))
            return;

        _permissions.Add(permission);
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new PermissionGrantedToRoleEvent
        {
            RoleId = Id,
            RoleName = Name!,
            PermissionId = permission.Id,
            PermissionName = permission.Name,
            GrantedAt = DateTime.UtcNow
        });
    }

    public void RemovePermission(Permission permission)
    {
        if (!_permissions.Any(p => p.Id == permission.Id))
            return;

        _permissions.RemoveAll(p => p.Id == permission.Id);
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new PermissionRevokedFromRoleEvent
        {
            RoleId = Id,
            RoleName = Name!,
            PermissionId = permission.Id,
            PermissionName = permission.Name,
            RevokedAt = DateTime.UtcNow
        });
    }

    public void AddPermissions(IEnumerable<Permission> permissions)
    {
        foreach (var permission in permissions)
        {
            AddPermission(permission);
        }
    }

    public void RemovePermissions(IEnumerable<Permission> permissions)
    {
        foreach (var permission in permissions)
        {
            RemovePermission(permission);
        }
    }

    public void ClearPermissions()
    {
        if (IsSystemRole)
            throw new InvalidOperationException("Cannot clear permissions from system role");

        var permissionsToRemove = _permissions.ToList();
        _permissions.Clear();
        DateModified = DateTime.UtcNow;

        foreach (var permission in permissionsToRemove)
        {
            AddDomainEvent(new PermissionRevokedFromRoleEvent
            {
                RoleId = Id,
                RoleName = Name!,
                PermissionId = permission.Id,
                PermissionName = permission.Name,
                RevokedAt = DateTime.UtcNow
            });
        }
    }

    public void ReplacePermissions(IEnumerable<Permission> newPermissions)
    {
        if (IsSystemRole)
            throw new InvalidOperationException("Cannot replace permissions for system role");

        ClearPermissions();
        AddPermissions(newPermissions);
    }

    public bool HasPermission(string permissionName)
    {
        return _permissions.Any(p => p.Name.Equals(permissionName, StringComparison.OrdinalIgnoreCase));
    }

    public bool HasPermission(Guid permissionId)
    {
        return _permissions.Any(p => p.Id == permissionId);
    }

    public bool HasAnyPermission(params string[] permissionNames)
    {
        return permissionNames.Any(HasPermission);
    }

    public bool HasAllPermissions(params string[] permissionNames)
    {
        return permissionNames.All(HasPermission);
    }

    public void AddUser(ApplicationUser user)
    {
        if (_users.Any(u => u.Id == user.Id))
            return;

        _users.Add(user);
        DateModified = DateTime.UtcNow;
    }

    public void RemoveUser(ApplicationUser user)
    {
        _users.RemoveAll(u => u.Id == user.Id);
        DateModified = DateTime.UtcNow;
    }

    public bool HasUser(string userId)
    {
        return _users.Any(u => u.Id == userId);
    }

    public int GetUserCount()
    {
        return _users.Count;
    }

    public int GetActiveUserCount()
    {
        return _users.Count(u => u.AccountStatus == Enums.AccountStatus.Active);
    }

    public bool CanBeDeleted()
    {
        return !IsSystemRole && !_users.Any();
    }

    public bool CanBeDeactivated()
    {
        return !IsSystemRole && IsActive;
    }

    public void MarkAsSystem()
    {
        IsSystemRole = true;
        DateModified = DateTime.UtcNow;
    }

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

    public override string ToString()
    {
        return $"{Name} ({(IsSystemRole ? "System" : "Custom")})";
    }
}
