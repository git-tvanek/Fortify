namespace Fortify.Domain.Events;

public sealed record RoleActivatedEvent : DomainEventBase
{
    public required string RoleId { get; init; }
    public required string RoleName { get; init; }
    public DateTime ActivatedAt { get; init; }
}