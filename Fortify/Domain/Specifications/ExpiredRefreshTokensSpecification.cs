using Fortify.Domain.Entities;

namespace Fortify.Domain.Specifications;

public class ExpiredRefreshTokensSpecification : SpecificationBase<ApplicationUser>
{
    public ExpiredRefreshTokensSpecification()
        : base(u => u.RefreshTokens.Any(rt => rt.ExpiresAt < DateTime.UtcNow && !rt.IsRevoked))
    {
        AddInclude(u => u.RefreshTokens);
    }
}