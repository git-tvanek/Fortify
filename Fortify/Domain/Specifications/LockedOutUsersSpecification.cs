using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class LockedOutUsersSpecification : SpecificationBase<ApplicationUser>
{
    public LockedOutUsersSpecification()
        : base(u => u.LockoutEndDate != null && u.LockoutEndDate > DateTime.UtcNow)
    {
        AddInclude(u => u.Roles);
        ApplyOrderByDescending(u => u.LockoutEndDate!);
    }
}