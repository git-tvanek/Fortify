namespace Fortify.Domain.Events;

public sealed record User2FADisabledEvent : DomainEventBase
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public DateTime DisabledAt { get; init; }
}