namespace Fortify.Domain.Events;

public sealed record PermissionGrantedToRoleEvent : DomainEventBase
{
    public required string RoleId { get; init; }
    public required string RoleName { get; init; }
    public required Guid PermissionId { get; init; }
    public required string PermissionName { get; init; }
    public DateTime GrantedAt { get; init; }
}