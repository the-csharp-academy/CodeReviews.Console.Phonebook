using System.Text.RegularExpressions;

namespace PhoneBook.Helpers;

public static partial class Validation
{
    [GeneratedRegex(@"^[\+]?[\d\s\-\(\)]{7,20}$")]
    private static partial Regex PhoneRegex();

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();

    [GeneratedRegex(@"^\+[1-9]\d{7,14}$")]
    private static partial Regex E164PhoneRegex();

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        return !string.IsNullOrWhiteSpace(phoneNumber) && PhoneRegex().IsMatch(phoneNumber);
    }

    public static bool IsValidEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && EmailRegex().IsMatch(email);
    }

    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Trim().Length is >= 1 and <= 100;
    }

    public static bool IsValidCategoryName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Trim().Length is >= 1 and <= 50;
    }

    public static bool IsValidE164PhoneNumber(string phoneNumber)
    {
        return !string.IsNullOrWhiteSpace(phoneNumber) && E164PhoneRegex().IsMatch(phoneNumber);
    }

    public static bool IsValidSmsMessage(string message)
    {
        return !string.IsNullOrWhiteSpace(message) && message.Length <= 1600;
    }
}
