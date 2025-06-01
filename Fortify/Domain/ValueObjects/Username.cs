using System.Text.RegularExpressions;

namespace Fortify.Domain.ValueObjects;

public readonly record struct Username
{
    private static readonly Regex UsernameRegex = new(@"^[a-zA-Z0-9_-]{3,20}$", RegexOptions.Compiled);

    public string Value { get; }

    public Username(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        if (!UsernameRegex.IsMatch(value))
            throw new ArgumentException("Username must be 3-20 characters long and contain only letters, numbers, underscores, and hyphens", nameof(value));

        Value = value;
    }

    public static implicit operator string(Username username) => username.Value;
    public static explicit operator Username(string value) => new(value);
    public override string ToString() => Value;
}