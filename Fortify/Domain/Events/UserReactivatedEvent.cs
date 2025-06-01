namespace Fortify.Domain.Events;

public sealed record UserReactivatedEvent : DomainEventBase
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public string? ReactivatedBy { get; init; }
    public DateTime ReactivatedAt { get; init; }
}