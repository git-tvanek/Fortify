using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class Users2FAEnabledSpecification : SpecificationBase<ApplicationUser>
{
    public Users2FAEnabledSpecification()
        : base(u => u.Is2FAEnabled)
    {
        AddInclude(u => u.Roles);
        ApplyOrderBy(u => u.UserName!);
    }
}