using Fortify.Domain.Common;

namespace Fortify.Domain.Entities;

public class Permission : EntityBase
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }

    public virtual ICollection<ApplicationRole> Roles { get; set; } = new HashSet<ApplicationRole>();
}