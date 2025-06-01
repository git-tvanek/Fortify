namespace Fortify.Domain.Constants;

public static class DefaultValues
{
    public const int PageSize = 10;
    public const int MaxPageSize = 100;
    public const int MinPasswordLength = 8;
    public const int MaxPasswordLength = 100;
    public const int MinUsernameLength = 3;  // Přidat
    public const int MaxUsernameLength = 20; // Přidat
    public const int RefreshTokenExpiryDays = 7;
    public const int MaxLoginAttempts = 5;
    public const int LockoutDurationMinutes = 15;
    public const int PasswordResetTokenExpiryHours = 24;  // Přidat
    public const int EmailConfirmationTokenExpiryDays = 7; // Přidat
}