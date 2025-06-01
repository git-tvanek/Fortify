using Fortify.Domain.Enums;

namespace Fortify.Domain.Events;

public sealed record UserAccountStatusChangedEvent : DomainEventBase
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public required AccountStatus OldStatus { get; init; }
    public required AccountStatus NewStatus { get; init; }
    public string? Reason { get; init; }
    public string? ChangedBy { get; init; }
    public DateTime ChangedAt { get; init; }
}