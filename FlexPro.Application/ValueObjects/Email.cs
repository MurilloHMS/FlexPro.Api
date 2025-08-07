using System.Text.RegularExpressions;

namespace Flexpro.Application.ValueObjects;
public class Email
{
    public string Address { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email address cannot be null or empty");

        if (!IsValidEmail(value))
            throw new ArgumentException("Invalid email address");

        Address = value;
    }

    public static bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && IsValidEmail(value);
    }

    private static bool IsValidEmail(string value)
    {
        return Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public override string ToString() => Address;

    public override bool Equals(object? obj)
        => obj is Email other && Address == other.Address;

    public override int GetHashCode() => Address.GetHashCode();

    public static implicit operator string(Email email) => email.Address;
    public static explicit operator Email(string value) => new(value);
}