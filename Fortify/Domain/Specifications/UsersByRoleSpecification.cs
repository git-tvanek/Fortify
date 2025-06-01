using Domain.Entities;

namespace Fortify.Domain.Specifications;

public class UsersByRoleSpecification : SpecificationBase<ApplicationUser>
{
    public UsersByRoleSpecification(string roleId)
        : base(u => u.Roles.Any(r => r.Id == roleId))
    {
        AddInclude(u => u.Roles);
    }

    public UsersByRoleSpecification(string roleId, int pageNumber, int pageSize)
        : base(u => u.Roles.Any(r => r.Id == roleId))
    {
        AddInclude(u => u.Roles);
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        ApplyOrderByDescending(u => u.DateCreated);
    }
}