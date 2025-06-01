using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class RolesWithUsersSpecification : SpecificationBase<ApplicationRole>
{
    public RolesWithUsersSpecification()
        : base(r => r.Users.Any())
    {
        AddInclude(r => r.Users);
        AddInclude(r => r.Permissions);
        ApplyOrderByDescending(r => r.Users.Count);
    }
}