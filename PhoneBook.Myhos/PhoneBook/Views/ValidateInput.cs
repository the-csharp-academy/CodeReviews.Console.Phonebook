using PhoneBook.Controller;
using PhoneNumbers;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook.Menus;

public class ValidateInput
{
    public static string ValidateEmail(string message)
    {
        while (true)
        {
            string email = AnsiConsole.Ask<string>(message);

            if (IsValidEmail(email)) 
            {
                if (ContactController.EmailExist(email))
                {
                    AnsiConsole.MarkupLine("That email already exist");
                }
                else 
                {
                    return email;
                }
            }

            AnsiConsole.MarkupLine("[red]Invalid email[/]");
        }
    }

    public static string ValidateName(string message)
    {
        while (true)
        {
            string name = AnsiConsole.Ask<string>(message);

            if (IsValidName(name)) 
            {
                if (ContactController.NameExist(name))
                {
                    AnsiConsole.MarkupLine("That name already exist");
                }
                else 
                {
                    return name;
                }
            }
               
            AnsiConsole.MarkupLine("[red]Invalid name[/]");
        }
    }

    public static string ValidatePhone(string message)
    {
        while (true)
        {
            string phoneNumber = AnsiConsole.Ask<string>(message);

            if (IsValidPhoneNumber(phoneNumber))
                return phoneNumber;

            AnsiConsole.MarkupLine("[red]Invalid Phone Number[/]");
        }
    }

    public static string ValidateCategoryName(string message) 
    {
        while (true)
        {
            string name = AnsiConsole.Ask<string>(message);

            if (IsValidCategoryName(name)) 
            {
                if (ContactController.NameExist(name))
                {
                    AnsiConsole.MarkupLine("That name already exist");
                }
                else 
                {
                    return name;
                }
            }
                
            AnsiConsole.MarkupLine("[red]Invalid name[/]");
        }
    }

    public static bool IsValidCategoryName(string? categoryName) 
    {
        if (string.IsNullOrWhiteSpace(categoryName))
            return false;

        categoryName = categoryName.Trim();

        if (categoryName.Length < 2 || categoryName.Length > 50)
            return false;

        return Regex.IsMatch(categoryName, @"^[\p{L}\p{N}]+(?:[ _-][\p{L}\p{N}]+)*$");
    }

    public static bool IsValidName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        name = name.Trim();

        if (name.Length < 2 || name.Length > 100)
            return false;

        return Regex.IsMatch(name, @"^[\p{L}]+(?:[ '-][\p{L}]+)*$");
    }

    public static bool IsValidPhoneNumber(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        if (!Regex.IsMatch(phoneNumber, @"^\+[1-9]\d+$"))
            return false;

        var phoneUtil = PhoneNumberUtil.GetInstance();

        try
        {
            var number = phoneUtil.Parse(phoneNumber, null);

            return phoneUtil.IsValidNumber(number);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }

    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        email = email.Trim();

        if (!Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
            return false;

        if (email.Contains(".."))
            return false;

        return true;
    }
}