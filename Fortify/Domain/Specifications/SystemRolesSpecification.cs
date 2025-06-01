using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class SystemRolesSpecification : SpecificationBase<ApplicationRole>
{
    public SystemRolesSpecification()
        : base(r => r.IsSystemRole)
    {
        AddInclude(r => r.Permissions);
        ApplyOrderBy(r => r.Name!);
    }
}