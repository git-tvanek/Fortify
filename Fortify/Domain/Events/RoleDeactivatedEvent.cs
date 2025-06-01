namespace Fortify.Domain.Events;

public sealed record RoleDeactivatedEvent : DomainEventBase
{
    public required string RoleId { get; init; }
    public required string RoleName { get; init; }
    public DateTime DeactivatedAt { get; init; }
}