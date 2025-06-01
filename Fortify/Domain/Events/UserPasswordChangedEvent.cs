namespace Fortify.Domain.Events;

public sealed record UserPasswordChangedEvent : DomainEventBase
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public DateTime ChangedAt { get; init; }
    public bool WasReset { get; init; }
}