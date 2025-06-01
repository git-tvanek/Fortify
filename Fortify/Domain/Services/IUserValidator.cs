using Fortify.Domain.Entities;

namespace Fortify.Domain.Services;

public interface IUserValidator
{
    Task<bool> CanCreateUserAsync(string username, string email, CancellationToken cancellationToken = default);
    Task<bool> CanUpdateUserAsync(string userId, string username, string email, CancellationToken cancellationToken = default);
    Task<bool> CanAssignRoleAsync(ApplicationUser user, ApplicationRole role, CancellationToken cancellationToken = default);
    Task<bool> CanBlockUserAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task<bool> CanDeactivateUserAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}