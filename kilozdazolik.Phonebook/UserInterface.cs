using System.Net.Http.Headers;
using kilozdazolik.Phonebook.Models;
using kilozdazolik.Phonebook.Services;
using Spectre.Console;

namespace kilozdazolik.Phonebook;

public class UserInterface
{
    static internal void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.MainMenuAction>()
                    .Title("What would you like to do?")
                    .AddChoices(Enums.MainMenuAction.ViewContacts, Enums.MainMenuAction.ViewCategories, Enums.MainMenuAction.Quit));

            switch (option)
            {
                case Enums.MainMenuAction.ViewContacts:
                    ContactsMenu();
                    break;
                case Enums.MainMenuAction.ViewCategories:
                    break;
                case Enums.MainMenuAction.Quit:
                    AnsiConsole.WriteLine("Bye!");
                    isAppRunning = false;
                    break;
            }
        }
    }
    
    private static void ContactsMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.ContactAction>()
                    .Title("Contacts Menu")
                    .AddChoices(Enums.ContactAction.ViewContact, Enums.ContactAction.AddContact, Enums.ContactAction.EditContact,
                        Enums.ContactAction.DeleteContact, Enums.ContactAction.Goback)
            );

            switch (option)
            {
                case Enums.ContactAction.ViewContact:
                     ContactService.GetContacts();
                    break;
                case Enums.ContactAction.AddContact:
                    ContactService.AddContact();
                    break;
                case Enums.ContactAction.DeleteContact:
                    ContactService.DeleteContact();
                    break;
                case Enums.ContactAction.EditContact:
                    ContactService.EditContact();
                    break;
                case Enums.ContactAction.Goback:
                    isAppRunning = false;
                    break;
            }
        }
    }

    public static void ShowContactsTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Name");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name);
        }
        
        AnsiConsole.Write(table);
        
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadLine();
        AnsiConsole.Clear();
    }
}