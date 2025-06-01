using Fortify.Domain.Entities;
using Fortify.Domain.Enums;

namespace Fortify.Domain.Specifications;

public class RecentlyActiveUsersSpecification : SpecificationBase<ApplicationUser>
{
    public RecentlyActiveUsersSpecification(int days)
        : base(u => u.AccountStatus == AccountStatus.Active &&
                    u.LastLoginDate != null &&
                    u.LastLoginDate >= DateTime.UtcNow.AddDays(-days))
    {
        AddInclude(u => u.Roles);
        ApplyOrderByDescending(u => u.LastLoginDate!);
    }
}