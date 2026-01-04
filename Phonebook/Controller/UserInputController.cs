using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Controller;

internal class UserInputController
{
    internal static string GetPhoneNumber()
    {
        string phoneNum;
        do
        {
            phoneNum = AnsiConsole.Prompt(
                new TextPrompt<string>("Please write the new Phone Number"));
        } while (IsValidPhoneNumber(phoneNum));
        var StringPhoneNum = phoneNum.ToString();
        return StringPhoneNum;
    }

    private static bool IsValidPhoneNumber(string PhoneNum)
    {
        if (PhoneNum.Length < 7)
        {
            AnsiConsole.MarkupLine("[red] Phone Number too short[/]");
            return true;
        }
        else if (!PhoneNum.All(char.IsAsciiDigit))
        {
            AnsiConsole.MarkupLine("[red] Phone Number can only include integers[/]");
            return true;
        }
        return false;

    }
    internal static string GetContactName() //User Should be able to call his contact whatever he wants
    {
        var ContactName = AnsiConsole.Prompt(
            new TextPrompt<string>("Please write the new Contact Name"));

        return ContactName;
    }
    internal static string GetCategoryName()
    {
        var CategoryName = AnsiConsole.Prompt(
            new TextPrompt<string>("Please write the new Category Name"));

        return CategoryName;
    }

    internal static string GetContactEmail()
    {
        string ContactEmail;
        do
        {
            ContactEmail = AnsiConsole.Prompt(
                new TextPrompt<string>("Please write the new Contact Email"));
        } while (IsValidEmailAddress(ContactEmail));

        return ContactEmail;
    }
    /*This only checks if there is @ in the email address, according to Microsoft Docs the only real way to check this is to send an email
    But it would make no sense to spam everyone you add to your contact list just to check if their email is real*/
    private static bool IsValidEmailAddress(string address) 
    {
        if (address != null && new EmailAddressAttribute().IsValid(address)) return false;
        AnsiConsole.MarkupLine("[red] Invalid Email Format, please try again[/]");
        return true;

    }

}
