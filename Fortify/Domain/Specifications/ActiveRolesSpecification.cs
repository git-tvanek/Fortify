using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class ActiveRolesSpecification : SpecificationBase<ApplicationRole>
{
    public ActiveRolesSpecification()
        : base(r => r.IsActive)
    {
        AddInclude(r => r.Permissions);
        AddInclude(r => r.Users);
        ApplyOrderBy(r => r.Name!);
    }

    public ActiveRolesSpecification(bool includeSystemRoles)
        : base(r => r.IsActive && (includeSystemRoles || !r.IsSystemRole))
    {
        AddInclude(r => r.Permissions);
        AddInclude(r => r.Users);
        ApplyOrderBy(r => r.Name!);
    }
}