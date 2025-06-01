namespace Fortify.Domain.Constants;

public static class DefaultValues
{
    public const int PageSize = 10;
    public const int MaxPageSize = 100;
    public const int MinPasswordLength = 8;
    public const int MaxPasswordLength = 100;
    public const int RefreshTokenExpiryDays = 7;
    public const int MaxLoginAttempts = 5;
    public const int LockoutDurationMinutes = 15;
}