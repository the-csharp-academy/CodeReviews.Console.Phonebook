using Microsoft.Extensions.Configuration;
using PhoneBook.Models;
using PhoneBook.Service;
using Spectre.Console;
using static PhoneBook.Menus.Enums;

namespace PhoneBook.Menus;

internal class UserInterface
{ 
    private readonly ContacService contacService;
    private readonly EmailService emailService;

    public UserInterface(IConfiguration configuration) 
    {
        contacService = new ContacService(configuration);
        emailService = new EmailService(configuration);
    }

    internal void MainMenu()
    {
        bool exit = true;

        while (exit)
        {
            Console.Clear();

            var options = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("Choose an option")
                .AddChoices(Enum.GetValues<MainMenuOptions>()));

            switch (options)
            {
                case MainMenuOptions.ManagedContacts:
                    ContactMenu();
                    break;
                case MainMenuOptions.ManagedCategories:
                    CategoryMenu();
                    break;
                case MainMenuOptions.Exit:
                    exit = false;
                    break;
            }
        }
    }

    private void ContactMenu() 
    {
        bool exit = true;

        while (exit)
        {
            Console.Clear();

            var options = AnsiConsole.Prompt(
                new SelectionPrompt<ContactMenuOptions>()
                .Title("Choose an option")
                .AddChoices(Enum.GetValues<ContactMenuOptions>()));

            switch (options)
            {
                case ContactMenuOptions.AddContact:
                    contacService.InsertContact();
                    break;
                case ContactMenuOptions.UpdateContact:
                    ContacService.UpdateContact();
                    break;
                case ContactMenuOptions.ViewContact:
                    ContacService.ViewContact();
                    break;
                case ContactMenuOptions.ViewAllContact:
                    ContacService.ViewAllContacts();
                    break;
                case ContactMenuOptions.DeleteContact:
                    ContacService.DeleteContact();
                    break;
                case ContactMenuOptions.SendEmail:
                    emailService.SendEmail();
                    break;
                case ContactMenuOptions.GoBack:
                    exit = false;
                    break;
            }
        }
    }

    private static void CategoryMenu() 
    {
        bool exit = true;

        while (exit)
        {
            Console.Clear();

            var options = AnsiConsole.Prompt(
                new SelectionPrompt<CategoryMenuOptions>()
                .Title("Choose an option")
                .AddChoices(Enum.GetValues<CategoryMenuOptions>()));

            switch (options)
            {
                case CategoryMenuOptions.AddCategory:
                    CategoryService.InsertCategory();
                    break;
                case CategoryMenuOptions.UpdateCategory:
                    CategoryService.UpdateCategory();
                    break;
                case CategoryMenuOptions.ViewCategory:
                    CategoryService.ViewCategory();
                    break;
                case CategoryMenuOptions.ViewAllCategory:
                    CategoryService.ViewAllCategories();
                    break;
                case CategoryMenuOptions.DeleteCategory:
                    CategoryService.DeleteCategory();
                    break;
                case CategoryMenuOptions.GoBack:
                    exit = false;
                    break;
            }
        }
    }

    public static void ShowContactsTable(List<Contact> contacts)
    {
        var table = new Table();

        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email");
        table.AddColumns("Category");

        foreach (Contact contact in contacts)
        {
            table.AddRow(
                contact.ContactId.ToString(),
                contact.Name,
                contact.PhoneNumber,
                contact.Email,
                contact.Category.Name);
        }

        table.Expand();
        table.BorderColor(Color.FromHex("#ACEDFF"));

        AnsiConsole.Write(table);

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();

        Console.Clear();
    }

    internal static void ViewContact(Contact contact)
    {
        var namePanel = new Panel($"{contact.Name}")
            .Header("Name")
            .BorderColor(Color.FromHex("#9D5C63"));

        var phonePanel = new Panel($"{contact.PhoneNumber}")
            .Header("Phone Number")
            .BorderColor(Color.FromHex("#9D5C63"));

        var emailPanel = new Panel($"{contact.Email}")
            .Header("Email")
            .BorderColor(Color.FromHex("#9D5C63"));

        var categoryPanel = new Panel($"{contact.Category.Name}")
            .Header("Category")
            .BorderColor(Color.FromHex("#9D5C63"));

        var outerPanel = new Panel(
            new Rows
            ( 
                namePanel,
                phonePanel,
                emailPanel,
                categoryPanel
            ))
        {
            Width = 40
        }
            .DoubleBorder();

        outerPanel.Header("Contact Info", Justify.Center);
        outerPanel.BorderColor(Color.FromHex("#9AD5CA"));
        outerPanel.Padding(3,2);

        AnsiConsole.Write(new Align(outerPanel,HorizontalAlignment.Center));

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();

        Console.Clear();
    }

    internal static void ShowCategory(Category category)
    {
        var namePanel = new Panel($"{category.Name}")
            .Header("Name")
            .BorderColor(Color.FromHex("#9D5C63"))
            .Padding(1,1);

        var outerPanel = new Panel(namePanel)
            .DottedBorder();

        outerPanel.Header("Category Info", Justify.Center);
        outerPanel.BorderColor(Color.FromHex("#9AD5CA"));
        outerPanel.Padding(1,1);

        AnsiConsole.Write(outerPanel);

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();

        Console.Clear();
    }

    internal static void ShowCategoriesTable(List<Category> categories)
    {
        var table = new Table();

        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (Category category in categories)
        {
            table.AddRow(
                category.CategoryId.ToString(),
                category.Name
                );
        }

        table.BorderColor(Color.FromHex("#ACEDFF"));
        table.Expand();

        AnsiConsole.Write(table);

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();

        Console.Clear();
    }
}