using Fortify.Domain.Entities;
using Fortify.Domain.Specifications;
using System.Linq.Expressions;

namespace Fortify.Domain.Interfaces;

public interface IRoleRepository
{
    Task<ApplicationRole?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<ApplicationRole?> GetByIdWithPermissionsAsync(string id, CancellationToken cancellationToken = default);
    Task<ApplicationRole?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationRole>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationRole>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationRole>> GetSystemRolesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationRole>> FindAsync(Expression<Func<ApplicationRole, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationRole>> FindAsync(SpecificationBase<ApplicationRole> specification, CancellationToken cancellationToken = default);
    Task<ApplicationRole> AddAsync(ApplicationRole role, CancellationToken cancellationToken = default);
    Task UpdateAsync(ApplicationRole role, CancellationToken cancellationToken = default);
    Task RemoveAsync(ApplicationRole role, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, string? excludeRoleId = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationRole>> GetRolesByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationRole>> GetRolesWithPermissionAsync(Guid permissionId, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<ApplicationRole, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationRole>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationRole>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<ApplicationRole, bool>> predicate, CancellationToken cancellationToken = default);
}