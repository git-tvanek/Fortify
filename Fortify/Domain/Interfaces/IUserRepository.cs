using Fortify.Domain.Entities;
using Fortify.Domain.Enums;
using System.Linq.Expressions;

namespace Fortify.Domain.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationUser>> FindAsync(Expression<Func<ApplicationUser, bool>> predicate, CancellationToken cancellationToken = default);
    Task<ApplicationUser> AddAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task RemoveAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(string email, string? excludeUserId = null, CancellationToken cancellationToken = default);
    Task<bool> IsUsernameUniqueAsync(string username, string? excludeUserId = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationUser>> GetByAccountStatusAsync(AccountStatus status, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleId, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationUser>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}