using System.Globalization;
using PhoneGallery.DreamFXX.Models;
using PhoneGallery.DreamFXX.Validation;
using Spectre.Console;

namespace PhoneGallery.DreamFXX.UserInput;

public class ContactInput
{
    public static string? GetValidatedInput(string askUser, Func<string, bool> validator)
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>($"[yellow]{askUser} or enter e, to exit[/]");
            if (input.Equals("e", StringComparison.OrdinalIgnoreCase))
                return null;

            if (validator(input))
            {
                return input;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid input. Please try again.[/]");
            }
        }
    }

    public static string GetNameInput()
    {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(GetValidatedInput("Enter name of the contact:", InputValidation.IsNameValid));
    }

    public static string GetEmailInput()
    {
        return GetValidatedInput("Enter contacts email address:", InputValidation.IsEmailValid);
    }

    public static string GetPhoneNumberInput()
    {
        return GetValidatedInput("Enter contacts phone number: ", InputValidation.IsPhoneNumberValid);
    }

    public static int GetCategorySelection(List<Category> categories)
    {
        var categorySelected = AnsiConsole.Prompt(
            new SelectionPrompt<Category>()
            .Title("Add contact to category:")
            .PageSize(4)
            .MoreChoicesText("[grey](Move cursor up and down to reveal more categories)[/]")
            .AddChoices(categories)
            .UseConverter(c => c.Name)
            );

        return categorySelected.Id;
    }

    public static Contact? GetSpecificContact(List<Contact> contacts)
    {
        if (contacts == null)
            return null;
        var contactSelected = AnsiConsole.Prompt(
            new SelectionPrompt<Contact>()
                .Title("[yellow]Select contact you wish to modify:[/]")
                .PageSize(10)
                .AddChoices(contacts)
                .UseConverter(c => c.Name)
        );

        return contactSelected;
    }


    public static bool ConfirmAction()
    {
        var confirmation = AnsiConsole.Ask<bool>("[green]Are you sure you want to change / delete this contact?[/]");
        return confirmation;
    }
}

