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
                    CategoriesMenu();
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
                    .AddChoices(Enums.ContactAction.ViewAllContacts, Enums.ContactAction.ViewContactsByCategory, Enums.ContactAction.AddContact, Enums.ContactAction.EditContact,
                        Enums.ContactAction.DeleteContact, Enums.ContactAction.Goback)
            );

            switch (option)
            {
                case Enums.ContactAction.ViewAllContacts:
                    ContactService.GetContacts();
                    break;
                case Enums.ContactAction.ViewContactsByCategory:
                    ContactService.GetContactsByCategory();
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

    static internal void CategoriesMenu()
    {
        var isMenuRunning = true;
        while (isMenuRunning)
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.CategoryAction>()
                    .Title("Categories Menu")
                    .AddChoices(Enums.CategoryAction.ViewCategories, Enums.CategoryAction.AddCategory, Enums.CategoryAction.EditCategory,
                        Enums.CategoryAction.DeleteCategory, Enums.CategoryAction.Goback)
            );

            switch (option)
            {
                case Enums.CategoryAction.ViewCategories:
                    CategoryService.GetCategories();
                    break;
                case Enums.CategoryAction.AddCategory:
                    CategoryService.InsertCategory();
                    break;
                case Enums.CategoryAction.DeleteCategory:
                    CategoryService.DeleteCategory();
                    break;
                case Enums.CategoryAction.EditCategory:
                    CategoryService.EditCategory();
                    break;
                case Enums.CategoryAction.Goback:
                    isMenuRunning = false;
                    break;
            }
        }
    }

    public static void ShowTable<T>(List<T> items) where T : class
    {
        var table = new Table();
        table.AddColumn("Name");

        if (items.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No items to display.[/]");
            AnsiConsole.WriteLine("Press any key to continue...");
            Console.ReadLine();
            return;
        }

        foreach (var item in items)
        {
            table.AddRow(item.GetType().GetProperty("Name").GetValue(item, null).ToString());
        }

        AnsiConsole.Write(table);

        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadLine();
        AnsiConsole.Clear();
    }
}