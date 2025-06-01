using Fortify.Domain.Entities;
using Fortify.Domain.Enums;

namespace Fortify.Domain.Specifications;

public class BlockedUsersSpecification : SpecificationBase<ApplicationUser>
{
    public BlockedUsersSpecification()
        : base(u => u.AccountStatus == AccountStatus.Blocked)
    {
        AddInclude(u => u.Roles);
        ApplyOrderByDescending(u => u.DateModified ?? u.DateCreated);
    }
}