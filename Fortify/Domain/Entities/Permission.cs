using Fortify.Domain.Common;
using Fortify.Domain.Events;

namespace Fortify.Domain.Entities;

public class Permission : EntityBase
{
    private readonly List<ApplicationRole> _roles = [];

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Category { get; private set; }
    public bool IsSystemPermission { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime DateCreated { get; private set; }
    public DateTime? DateModified { get; private set; }

    public virtual IReadOnlyCollection<ApplicationRole> Roles => _roles.AsReadOnly();

    protected Permission() { }

    public Permission(string name, string category, string? description = null, bool isSystemPermission = false)
    {
        Id = Guid.NewGuid();
        Name = name;
        Category = category;
        Description = description;
        IsSystemPermission = isSystemPermission;
        DateCreated = DateTime.UtcNow;

        AddDomainEvent(new PermissionCreatedEvent
        {
            PermissionId = Id,
            PermissionName = Name,
            Category = Category,
            Description = Description,
            IsSystemPermission = IsSystemPermission,
            CreatedAt = DateCreated
        });
    }

    public void UpdateDetails(string name, string category, string? description)
    {
        if (IsSystemPermission)
            throw new InvalidOperationException("Cannot update system permission details");

        Name = name;
        Category = category;
        Description = description;
        DateModified = DateTime.UtcNow;

        AddDomainEvent(new PermissionUpdatedEvent
        {
            PermissionId = Id,
            PermissionName = Name,
            Category = Category,
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
    }

    public void Deactivate()
    {
        if (!IsActive || IsSystemPermission)
            return;

        IsActive = false;
        DateModified = DateTime.UtcNow;
    }
}