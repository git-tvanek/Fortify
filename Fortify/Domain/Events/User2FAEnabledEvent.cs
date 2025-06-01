namespace Fortify.Domain.Events;

public sealed record User2FAEnabledEvent : DomainEventBase
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public DateTime EnabledAt { get; init; }
}