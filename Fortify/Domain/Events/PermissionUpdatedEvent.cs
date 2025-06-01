namespace Fortify.Domain.Events;

public sealed record PermissionUpdatedEvent : DomainEventBase
{
    public required Guid PermissionId { get; init; }
    public required string PermissionName { get; init; }
    public required string Category { get; init; }
    public string? Description { get; init; }
    public DateTime UpdatedAt { get; init; }
}