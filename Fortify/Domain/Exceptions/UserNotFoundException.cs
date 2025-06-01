namespace Fortify.Domain.Exceptions;

public class UserNotFoundException : DomainException
{
    public string? UserId { get; }
    public string? Username { get; }
    public string? Email { get; }

    public UserNotFoundException(string userId)
        : base($"User with ID '{userId}' was not found.")
    {
        UserId = userId;
    }

    public static UserNotFoundException ByUsername(string username)
    {
        return new UserNotFoundException($"User with username '{username}' was not found.")
        {
            Username = username
        };
    }

    public static UserNotFoundException ByEmail(string email)
    {
        return new UserNotFoundException($"User with email '{email}' was not found.")
        {
            Email = email
        };
    }

    private UserNotFoundException(string message, bool dummy) : base(message) { }
}