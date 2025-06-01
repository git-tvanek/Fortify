namespace Fortify.Domain.Events;

public sealed record RoleCreatedEvent : DomainEventBase
{
    public required string RoleId { get; init; }
    public required string RoleName { get; init; }
    public string? Description { get; init; }
    public required bool IsSystemRole { get; init; }
    public DateTime CreatedAt { get; init; }
}