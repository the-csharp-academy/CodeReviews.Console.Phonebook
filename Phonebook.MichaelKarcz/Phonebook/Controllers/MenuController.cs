using Spectre.Console;

namespace Phonebook.Controllers
{
    internal static class MenuController
    {
        internal static void RunMenuLoop()
        {
            int menuChoiceNumber = -1;

            while (menuChoiceNumber != 0)
            {
                string menuChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("- PHONEBOOK - \nWhat would you like to do?")
                    .PageSize(7)
                    .AddChoices(new[]
                    {
                        "1) View contacts",
                        "2) Add a contact",
                        "3) Edit a contact",
                        "4) Delete a contact",
                        "0) [grey]Exit the application[/]"
                    }));

                menuChoiceNumber = Int32.Parse(menuChoice.Substring(0, 1));

                AnsiConsole.Clear();

                switch (menuChoiceNumber)
                {
                    case 0:
                        AnsiConsole.WriteLine("\nGoodbye!");
                        break;
                    case 1:
                        ContactController.ViewContacts();
                        break;
                    case 2:
                        ContactController.AddContact();
                        break;
                    case 3:
                        ContactController.EditContact();
                        break;
                    case 4:
                        ContactController.DeleteContact();
                        break;
                    default:
                        AnsiConsole.WriteLine("\nAn error has occurred processing your request. The application will now close. Goodbye!");
                        menuChoiceNumber = 0;
                        break;
                }
            }
        }
    }
}
