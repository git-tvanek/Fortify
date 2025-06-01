using Domain.Entities;
using Fortify.Domain.Enums;

namespace Fortify.Domain.Specifications;

public class ActiveUsersSpecification : SpecificationBase<ApplicationUser>
{
    public ActiveUsersSpecification()
        : base(u => u.AccountStatus == AccountStatus.Active)
    {
        AddInclude(u => u.Roles);
        ApplyOrderBy(u => u.UserName!);
    }

    public ActiveUsersSpecification(int pageNumber, int pageSize)
        : base(u => u.AccountStatus == AccountStatus.Active)
    {
        AddInclude(u => u.Roles);
        ApplyOrderBy(u => u.UserName!);
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }
}