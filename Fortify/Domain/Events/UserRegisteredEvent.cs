namespace Fortify.Domain.Events;

public sealed record UserRegisteredEvent : DomainEventBase
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public DateTime RegisteredAt { get; init; }
}