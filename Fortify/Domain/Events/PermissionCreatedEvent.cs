namespace Fortify.Domain.Events;

public sealed record PermissionCreatedEvent : DomainEventBase
{
    public required Guid PermissionId { get; init; }
    public required string PermissionName { get; init; }
    public required string Category { get; init; }
    public string? Description { get; init; }
    public required bool IsSystemPermission { get; init; }
    public DateTime CreatedAt { get; init; }
}