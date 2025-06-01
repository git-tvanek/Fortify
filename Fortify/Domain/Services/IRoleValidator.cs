using Fortify.Domain.Entities;

namespace Fortify.Domain.Services;

public interface IRoleValidator
{
    Task<bool> CanCreateRoleAsync(string roleName, CancellationToken cancellationToken = default);
    Task<bool> CanUpdateRoleAsync(string roleId, string roleName, CancellationToken cancellationToken = default);
    Task<bool> CanDeleteRoleAsync(ApplicationRole role, CancellationToken cancellationToken = default);
    Task<bool> CanAssignPermissionAsync(ApplicationRole role, Permission permission, CancellationToken cancellationToken = default);
}