using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Views;

internal class TableVisualisation
{
    internal static void PrintUsersList(List<User> list)
    {
        var table = new Table()
            .Border(TableBorder.Heavy)
            .BorderColor(Color.Blue)
            .ShowRowSeparators();

        table.AddColumns("Name", "Category", "Email", "Phone number", "Address");

        foreach (var user in list)
        {
            table.AddRow(user.Name, user.Category.ToString(), user.Email, user.PhoneNumber, user.Address ?? "-");
        }

        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("Press any key to continue: ");
        Console.ReadKey();
    }

    internal static void ShowUser(User user)
    {
        Panel panel = new(
@$"Id: {user.Id}
Name: {user.Name}
Category: {user.Category}
Email: {user.Email}
Phone Number: {user.PhoneNumber}
Address: {user.Address}");

        panel.Header = new("User Info");
        panel.Padding = new(2);

        AnsiConsole.Write(panel);

        AnsiConsole.MarkupLine("Press any key to continue: ");
        Console.ReadKey();
    }
}
