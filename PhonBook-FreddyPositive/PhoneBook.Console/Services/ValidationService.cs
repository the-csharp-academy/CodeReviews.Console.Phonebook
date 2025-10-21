using PhoneBook.Controller;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook.Services;

internal class ValidationService
{
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    internal static string ValidateName(string name)
    {
        Regex validName = new Regex(@"^[A-Za-z\s]+$");

        while (string.IsNullOrWhiteSpace(name) || !validName.IsMatch(name))
        {
            name = AnsiConsole.Ask<string>("Enter a Valid Contact's Name");
        }
        return name;
    }

    internal static string ValidatePhoneNumber(string phoneNumber)
    {
        while (true)
        {
            phoneNumber = IsValidPhoneNumber(phoneNumber);

            if (!PhoneBookController.IsPhoneNumberAlreadyExist(phoneNumber))
                return phoneNumber;

            phoneNumber = AnsiConsole.Ask<string>("Phone Number already registered. Please try a new phone number");
        }
    }

    internal static string ValidateEmailAddress(string emailAddress)
    {
        while (true)
        {
            emailAddress = IsValidEmail(emailAddress);

            if (!PhoneBookController.IsEmailAddressAlreadyExist(emailAddress))
                return emailAddress;

            emailAddress = AnsiConsole.Ask<string>("Email address already registered. Please try a new email address:");
        }
    }

    private static string IsValidEmail(string email)
    {
        while (!EmailRegex.IsMatch(email))
        {
            email = AnsiConsole.Ask<string>("Enter a valid email address:");
        }
        return email;
    }

    private static string IsValidPhoneNumber(string phoneNumber)
    {
        while (phoneNumber.Length != 10 || string.IsNullOrWhiteSpace(phoneNumber))
        {
            phoneNumber = AnsiConsole.Ask<string>("Enter a Valid Phone Number with 10 digits");
        }
        return phoneNumber;
    }
}
