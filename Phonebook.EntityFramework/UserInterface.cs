using Phonebook.EntityFramework.Controllers;
using Phonebook.EntityFramework.Domain.Entities;
using Spectre.Console;
using System.Globalization;

namespace Phonebook.EntityFramework;

static internal class UserInterface{
   
    internal static void ShowContact(Contact contact)
    {
        var panel = new Panel($@"Id: {contact.Id}
Name: {contact.Name}
Email: {contact.Email}
Phone number: {contact.PhoneNumber}
Category: {contact.Category.Name}");
        panel.Header= new PanelHeader("Contact Details");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        Console.WriteLine("Press enter to continue:");
        Console.ReadLine();
        Console.Clear();
    }

    static internal void ShowContactTable(List<Contact> contacts)
    {
        var table = new Spectre.Console.Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");
        table.AddColumn("Category");
        foreach (var contact in contacts)
        {
            var categoryName = contact.Category?.Name ?? "<none>";
            table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber, categoryName);
        }
        AnsiConsole.Write(table);

        Console.WriteLine("Press enter to continue:");
        Console.ReadLine();
        Console.Clear();
    }

    internal static void ShowCategory(Category category)
    {
        var panel = new Panel($@"Id: {category.Id}
Name: {category.Name}");
        panel.Header = new PanelHeader("Category Details");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        Console.WriteLine("Press enter to continue:");
        Console.ReadLine();
        Console.Clear();
    }

    static internal void ShowCategoryTable(List<Category> categories)
    {
        var table = new Spectre.Console.Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        foreach (var category in categories)
        {
            table.AddRow(category.Id.ToString(), category.Name);
        }
        AnsiConsole.Write(table);

        Console.WriteLine("Press enter to continue:");
        Console.ReadLine();
        Console.Clear();
    }
    static public Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetContacts();
        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(x => x.Name == option).Id;
        var contact = ContactController.GetContactById(id);

        return contact;
    }

    static public Category GetCategoryOptionInput()
    {
        var categories = CategoryController.GetCategories();
        var categoriessArray = categories.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Category")
            .AddChoices(categoriessArray));
        var id = categories.Single(x => x.Name == option).Id;
        var category = CategoryController.GetCategoryById(id);

        return category;
    }
    static public void ShowEMail(String to, String subject, String body)
    {
        Console.WriteLine("\n=== SENDING EMAIL ===");
        Console.WriteLine($"To: {to}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");
        Console.WriteLine("=====================\n");
        Console.WriteLine("Press enter to continue:");
        Console.ReadLine();
        Console.Clear();
    }
}
