using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class UnusedPermissionsSpecification : SpecificationBase<Permission>
{
    public UnusedPermissionsSpecification()
        : base(p => !p.Roles.Any())
    {
        AddInclude(p => p.Roles);
        ApplyOrderBy(p => p.Category);
    }
}