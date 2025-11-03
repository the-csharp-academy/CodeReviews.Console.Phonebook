using kilozdazolik.Phonebook.Models;
using Spectre.Console;

namespace kilozdazolik.Phonebook;

public class Helper
{
    internal static bool ConfirmMessage(string message, string element, string color = "red")
    {
        var confirm = AnsiConsole.Confirm($"Are you sure you want to {message} [{color}]{element}[/]?");

        return confirm;
    }
    
    internal static string GetNameInput()
    {
        string name;
        name = AnsiConsole.Ask<string>("Please enter the name:");
        return name.Trim();
    }

    internal static string GetEmailInput()
    {
        string email;
        bool isEmailValid = false;
    
        do
        {
            email = AnsiConsole.Ask<string>("Please enter the email:");

            if (Validator.IsValidEmail(email))
            {
                isEmailValid = true;
            }
            else
            {
                AnsiConsole.WriteLine("[red]Invalid email, please try again[/]");
            }
        
        } while (!isEmailValid);
    
        return email.Trim();
    }

    internal static string GetPhoneNumberInput()
    {
        AnsiConsole.MarkupLine("[yellow]Please enter phone number with country code (e.g., +36201234567, +14155552671)[/]");
        string phoneNumber;
        bool isPhoneNumberValid = false;
    
        do
        {
            phoneNumber = AnsiConsole.Ask<string>("Please enter the phone number:");

            if (Validator.IsValidPhoneNumber(phoneNumber))
            {
                isPhoneNumberValid = true;
            }
            else
            {
                AnsiConsole.MarkupLine(
                    "[red]Invalid phone number format. Make sure the format is good![/]");
            }
        
        } while (!isPhoneNumberValid);
    
        return phoneNumber.Trim();
    }
    
}