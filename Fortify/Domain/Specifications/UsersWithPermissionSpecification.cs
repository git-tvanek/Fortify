using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class UsersWithPermissionSpecification : SpecificationBase<ApplicationUser>
{
    public UsersWithPermissionSpecification(Guid permissionId)
        : base(u => u.Roles.Any(r => r.Permissions.Any(p => p.Id == permissionId)))
    {
        AddInclude(u => u.Roles);
        AddInclude("Roles.Permissions");
    }
}