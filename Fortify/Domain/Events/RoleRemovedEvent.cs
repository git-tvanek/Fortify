namespace Fortify.Domain.Events;

public sealed record RoleRemovedEvent : DomainEventBase
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public required string RoleId { get; init; }
    public required string RoleName { get; init; }
    public string? RemovedBy { get; init; }
    public DateTime RemovedAt { get; init; }
}