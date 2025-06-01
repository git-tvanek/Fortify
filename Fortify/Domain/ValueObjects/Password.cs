using System.Text.RegularExpressions;
using Fortify.Domain.Constants;

namespace Fortify.Domain.ValueObjects;

public readonly record struct Password
{
    private static readonly Regex PasswordRegex = new(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        RegexOptions.Compiled);

    public string Value { get; }

    public Password(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        if (value.Length < DefaultValues.MinPasswordLength)
            throw new ArgumentException($"Password must be at least {DefaultValues.MinPasswordLength} characters long", nameof(value));

        if (value.Length > DefaultValues.MaxPasswordLength)
            throw new ArgumentException($"Password must not exceed {DefaultValues.MaxPasswordLength} characters", nameof(value));

        if (!PasswordRegex.IsMatch(value))
            throw new ArgumentException("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character", nameof(value));

        Value = value;
    }

    public static implicit operator string(Password password) => password.Value;
    public static explicit operator Password(string value) => new(value);
    public override string ToString() => "********";
}