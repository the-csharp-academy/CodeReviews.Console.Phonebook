using Phone_Book.Models;
using Spectre.Console;

namespace Phone_Book.Views;

internal class UserInterface
{
    internal static void ShowAllContactsTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");
        table.AddColumn("Category");

        foreach (var contact in contacts)
            table.AddRow(
                contact.Id.ToString(),
                contact.Name,
                contact.Email,
                contact.Phone,
                contact.Category.CategoryName
            );

        AnsiConsole.Write(table);
    }

    public static void ShowContactTable(Contact contact)
    {
        var panel = new Panel(
            $"Id: {contact.Id} \nName: {contact.Name} \nEmail: {contact.Email} \nPhone Number: {contact.Phone} \nCategory: {contact.Category.CategoryName}"
        );
        panel.Header = new PanelHeader("Contact Information: ");
        panel.Padding = new Padding(2);

        AnsiConsole.Write(panel);
    }

    public static void ShowCategoriesTable(List<Category> categories)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (var category in categories) table.AddRow(category.CategoryId.ToString(), category.CategoryName);

        AnsiConsole.Write(table);
    }
}