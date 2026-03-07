using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using System.Net.Mail;
using static PhoneBook.Models.Enums;

namespace PhoneBook;

public static class Validator
{
    public static UserCategories ChooseCategory()
    {
        var category = AnsiConsole.Prompt(new SelectionPrompt<UserCategories>()
            .Title("Choose the category: ")
            .AddChoices(Enum.GetValues<UserCategories>()));

        return category;
    }

    public static string GetEmailInput()
    {
        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your [green]email[/]: ")
            .Validate(input =>
            ValidateEmail(input)
            ? ValidationResult.Success()
            : ValidationResult.Error("Please enter a valid [red]email[/]")));

        return email;
    }

    public static string GetPhoneNumber()
    {
        var phoneNumber = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your [blue]phone number[/]: ")
            .Validate(input =>
            ValidatePhoneNumber(input)
            ? ValidationResult.Success()
            : ValidationResult.Error("Please enter a valid [red]number[/]")));

        return phoneNumber;
    }

    public static string GetStringInput(string msg)
    {
        var input = AnsiConsole.Prompt(
            new TextPrompt<string>(msg)
            .Validate(x =>
            ValidateString(x)
            ? ValidationResult.Success()
            : ValidationResult.Error("Enter a valid string input")));

        return input.Trim();
    }

    public static bool ValidateEmail(string email)
    {
        return MailAddress.TryCreate(email, out _);
    }

    public static bool ValidatePhoneNumber(string phoneNumber)
    {
        bool hasMoreThanOne = phoneNumber.Count('+') > 1;
        bool hasOne = phoneNumber.Count('+') == 1;
        bool startsWith = phoneNumber.StartsWith('+');
        bool isPhoneValid = phoneNumber.Trim().All(c => char.IsDigit(c) || c == '+');

        if (hasMoreThanOne)
            return false;

        if (hasOne && !startsWith)
            return false;

        if (isPhoneValid || (isPhoneValid && hasOne && startsWith))
            return true;

        return false;
    }

    public static bool ValidateString(string input)
    {
        return !string.IsNullOrWhiteSpace(input);
    }

}
