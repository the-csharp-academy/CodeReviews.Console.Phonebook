using Spectre.Console;

namespace PhoneBook.EntityFramework
{
   static internal class UserInterface
    {
        internal static void ShowContact(Contact contact)
        {
            var panel = new Panel($@"Id: {contact.Id}
Name: {contact.Name}");
            panel.Header = new PanelHeader("Contact Info");
            panel.Padding = new Padding(2, 2, 2, 2);

            AnsiConsole.Write(panel);

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
            Console.Clear();

        }

        static internal void ShowContactTable(List<Contact> contacts)
        {
            var table = new Table();

            table.AddColumn("Id");
            table.AddColumn("Name");
            table.AddColumn("Email");
            table.AddColumn("Number");


            foreach (var item in contacts)
            {
                table.AddRow(item.Id.ToString(), item.Name,item.Email, item.Phone.ToString());

            }
            
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
            Console.Clear();

            AnsiConsole.Write(table);
        }
    }
}
