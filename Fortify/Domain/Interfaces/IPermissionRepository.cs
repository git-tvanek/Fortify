using Fortify.Domain.Entities;
using Fortify.Domain.Specifications;
using System.Linq.Expressions;

namespace Fortify.Domain.Interfaces;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Permission?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Permission>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Permission>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
    Task<IEnumerable<Permission>> FindAsync(Expression<Func<Permission, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Permission>> FindAsync(SpecificationBase<Permission> specification, CancellationToken cancellationToken = default);
    Task<Permission> AddAsync(Permission permission, CancellationToken cancellationToken = default);
    Task UpdateAsync(Permission permission, CancellationToken cancellationToken = default);
    Task RemoveAsync(Permission permission, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, Guid? excludePermissionId = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(string roleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<Permission, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Permission>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<IEnumerable<Permission>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Permission, bool>> predicate, CancellationToken cancellationToken = default);
}