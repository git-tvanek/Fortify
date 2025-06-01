namespace Fortify.Domain.Events;

public sealed record RoleUpdatedEvent : DomainEventBase
{
    public required string RoleId { get; init; }
    public required string RoleName { get; init; }
    public string? Description { get; init; }
    public DateTime UpdatedAt { get; init; }
}