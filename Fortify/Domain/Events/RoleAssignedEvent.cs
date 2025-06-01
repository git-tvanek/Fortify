namespace Fortify.Domain.Events;

public sealed record RoleAssignedEvent : DomainEventBase
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public required string RoleId { get; init; }
    public required string RoleName { get; init; }
    public string? AssignedBy { get; init; }
    public DateTime AssignedAt { get; init; }
}