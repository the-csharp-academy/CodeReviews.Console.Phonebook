using Phonebook.Controller;
using Spectre.Console;

namespace Phonebook.View;

internal class UserInterface
{
    internal static void MainMenu()
    {
        Console.Clear();
        List<string> MenuChoices = new() { "Add Menu", "Update Menu", "Delete Menu", "View Contact", "View Categories", "Send Email", "Exit" };
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Contacts")
            .AddChoices(MenuChoices));

        switch (choice)
        {
            case "Add Menu":
                AddMenu();
                break;
            case "Update Menu":
                UpdateContact();
                break;
            case "Delete Menu":
                DeleteMenu();
                break;
            case "View Contact":
                var contact = PhonebookController.ChooseContact();
                AnsiConsole.MarkupLine($"[yellow][aqua]{contact.ContactName}[/] - Phone Number: [aqua]{contact.PhoneNumber}[/], Email: [aqua]{contact.Email}[/][/]");
                if (contact.CategoryName != null) AnsiConsole.MarkupLine($"[yellow]Category:[/][aqua]{contact.CategoryName}[/]");
                Console.ReadKey();
                MainMenu();
                break;
            case "View Categories":
                var ContactFromCategory = PhonebookController.ChooseContactFromCategory();
                if (ContactFromCategory == null)
                {
                    AnsiConsole.MarkupLine("[red]No contacts in said category, returning to Main Menu[/]");
                    Console.ReadKey();
                    MainMenu();
                }
                AnsiConsole.MarkupLine($"[yellow][aqua]{ContactFromCategory.ContactName}[/] - Phone Number: [aqua]{ContactFromCategory.PhoneNumber}[/], Email: [aqua]{ContactFromCategory.Email}[/], Category: [aqua]{ContactFromCategory.CategoryName}[/][/]");
                Console.ReadKey();
                MainMenu();
                break;
            case "Send Email":
                PhonebookController.SendEmail();
                Console.ReadKey();
                MainMenu();
                break;
            case "Exit":
                Environment.Exit(0);
                Console.WriteLine("Closing the app!");
                break;
        }
    }
    internal static void AddMenu()
    {
        Console.Clear();
        List<string> MenuChoices = new() {"Add Contact", "Add Category", "Cancel" };
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Add Menu")
            .AddChoices(MenuChoices));

        switch (choice)
        {
            case "Add Contact":
                PhonebookController.AddContact();
                Console.ReadKey();
                MainMenu();
                break;
            case "Add Category":
                PhonebookController.AddCategory();
                Console.ReadKey();
                MainMenu();
                break;
            case "Cancel":
                MainMenu();
                break;
        }
    }
    internal static void DeleteMenu()
    {
        Console.Clear();
        List<string> MenuChoices = new() {"Delete Contact", "Delete Category", "Cancel" };
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Delete Menu")
            .AddChoices(MenuChoices));

        switch (choice)
        {
            case "Delete Contact":
                PhonebookController.DeleteContact();
                Console.ReadKey();
                MainMenu();
                break;
            case "Delete Category":
                PhonebookController.DeleteCategory();
                Console.ReadKey();
                MainMenu();
                break;
            case "Cancel":
                MainMenu();
                break;
        }
    }

    private static void UpdateContact()
    {
        Console.Clear();
        List<string> MenuChoices = new() { "Update Contact Name", "Update Contact Email", "Update Phone Number", "Update Category", "Cancel" };
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Update Contacts")
            .AddChoices(MenuChoices));

        switch (choice)
        {
            case "Update Contact Name":
                
                PhonebookController.UpdateContactName();
                Console.ReadKey();
                MainMenu();
                break;
            case "Update Contact Email":
                PhonebookController.UpdateContactEmail();
                Console.ReadKey();
                MainMenu();
                break;
            case "Update Phone Number":
                PhonebookController.UpdateContactPhoneNumber();
                Console.ReadKey();
                MainMenu();
                break;
            case "Update Category":
                PhonebookController.UpdateContactCategory();
                Console.ReadKey();
                MainMenu();
                break;
            case "Cancel":
                MainMenu();
                break;
        }
    }
}
