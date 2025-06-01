namespace Fortify.Domain.Events;

public sealed record PermissionRevokedFromRoleEvent : DomainEventBase
{
    public required string RoleId { get; init; }
    public required string RoleName { get; init; }
    public required Guid PermissionId { get; init; }
    public required string PermissionName { get; init; }
    public DateTime RevokedAt { get; init; }
}