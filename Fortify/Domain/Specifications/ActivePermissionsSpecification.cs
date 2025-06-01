using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class ActivePermissionsSpecification : SpecificationBase<Permission>
{
    public ActivePermissionsSpecification()
        : base(p => p.IsActive)
    {
        AddInclude(p => p.Roles);
        ApplyOrderBy(p => p.Category);
    }

    public ActivePermissionsSpecification(string category)
        : base(p => p.IsActive && p.Category == category)
    {
        AddInclude(p => p.Roles);
        ApplyOrderBy(p => p.Name);
    }
}