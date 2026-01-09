using PhoneBook.TerrenceLGee.Extensions;
using PhoneBook.TerrenceLGee.PhonebookAppUi.Helpers;
using PhoneBook.TerrenceLGee.PhonebookAppUi.Interfaces;
using PhoneBook.TerrenceLGee.PhonebookAppUi.Menus;
using Spectre.Console;

namespace PhoneBook.TerrenceLGee.PhonebookAppUi;

public class PhonebookApp
{
    private readonly IPhonebookUi _phonebookUi;

    public PhonebookApp(IPhonebookUi phonebookUi)
    {
        _phonebookUi = phonebookUi;
    }

    public async Task Run()
    {
        DisplayMessage("Phonebook App");
        UiHelpers.PressAnyKeyToContinue();

        var isUserFinished = false;

        while (!isUserFinished)
        {
            var choice = GetUserChoice();

            switch (choice)
            {
                case MainMenu.AddContact:
                    await _phonebookUi.AddContactAsync();
                    break;
                case MainMenu.UpdateContact:
                    await _phonebookUi.UpdateContactAsync();
                    break;
                case MainMenu.DeleteContact:
                    await _phonebookUi.DeleteContactAsync();
                    break;
                case MainMenu.ViewContact:
                    await _phonebookUi.ViewContactAsync();
                    break;
                case MainMenu.ViewAllContacts:
                    await _phonebookUi.ViewAllContactsAsync();
                    break;
                case MainMenu.SendEmail:
                    await _phonebookUi.SendEmailAsync();
                    break;
                case MainMenu.Exit:
                    isUserFinished = true;
                    break;
                default:
                    UiHelpers.PressAnyKeyToContinueError("You entered an invalid choice");
                    break;
            }
        }

        DisplayMessage("Goodbye");
        UiHelpers.PressAnyKeyToContinue();
    }

    private static MainMenu GetUserChoice()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<MainMenu>()
            .Title("[DarkOrange]Please choose one of the following options[/]")
            .AddChoices(Enum.GetValues<MainMenu>())
            .UseConverter(choice => choice.GetDisplayName()));
    }

    private static void DisplayMessage(string message)
    {
        AnsiConsole.Write(
            new FigletText($"{message}")
            .Centered()
            .Color(Color.Fuchsia));
    }
}
