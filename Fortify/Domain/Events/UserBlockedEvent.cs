namespace Fortify.Domain.Events;

public sealed record UserBlockedEvent : DomainEventBase
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public string? Reason { get; init; }
    public string? BlockedBy { get; init; }
    public DateTime BlockedAt { get; init; }
}