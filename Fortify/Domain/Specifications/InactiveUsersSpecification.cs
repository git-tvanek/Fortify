using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class InactiveUsersSpecification : SpecificationBase<ApplicationUser>
{
    public InactiveUsersSpecification(int inactiveDays)
        : base(u => u.LastLoginDate == null || u.LastLoginDate < DateTime.UtcNow.AddDays(-inactiveDays))
    {
        AddInclude(u => u.Roles);
        ApplyOrderBy(u => u.LastLoginDate ?? DateTime.MinValue);
    }
}