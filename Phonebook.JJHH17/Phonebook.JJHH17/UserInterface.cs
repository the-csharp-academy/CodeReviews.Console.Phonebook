using System;
using System.Linq;
using Spectre.Console;

namespace Phonebook.JJHH17
{
    public class UserInterface
    {

        enum MenuOptions
        {
            AddEntry,
            Read,
            Delete,
            Update,
            SendEmail,
            SendSMS,
            Exit,
        }

        public static void Menu()
        {
            bool running = true;
            while (running)
            {
                AnsiConsole.MarkupLine("[bold blue]Welcome to the Phonebook Application![/]");
                Console.Clear();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOptions>()
                        .Title("Select an option:")
                        .AddChoices(Enum.GetValues<MenuOptions>()));

                switch (choice)
                {
                    case MenuOptions.AddEntry:
                        Console.Clear();
                        AddEntry();
                        break;

                    case MenuOptions.Read:
                        Console.Clear();
                        ReadEntries();
                        AnsiConsole.MarkupLine("[bold green]Press any key to continue...[/]");
                        Console.ReadKey();
                        break;

                    case MenuOptions.Delete:
                        Console.Clear();
                        DeleteEntry();
                        break;

                    case MenuOptions.Update:
                        Console.Clear();
                        UpdateEntry();
                        break;

                    case MenuOptions.SendEmail:
                        Console.Clear();
                        AnsiConsole.MarkupLine("[bold blue]Compose and send an email[/]");
                        SendEmail();
                        break;

                    case MenuOptions.SendSMS:
                        Console.Clear();
                        AnsiConsole.MarkupLine("[bold blue]Compose and send an SMS[/]");
                        SendSMS();
                        break;

                    case MenuOptions.Exit:
                        Console.Clear();
                        running = false;
                        AnsiConsole.MarkupLine("[bold green]Thank you for using the Phonebook Application![/]");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void AddEntry()
        {
            Console.WriteLine("Enter Name:");
            string name = Console.ReadLine();
            string email = EnterEmail();
            string phoneNumber = EnterPhoneNumber();
            Console.WriteLine("Enter Category (optional):");
            string category = Console.ReadLine();

            using (var context = new PhoneBookContext())
            {
                var newEntry = new PhoneBook
                {
                    Name = name,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Category = category
                };
                context.PhoneBooks.Add(newEntry);
                context.SaveChanges();
            }

            Console.WriteLine("Entry added successfully. Enter any key to continue...");
            Console.ReadKey();
        }

        public static void ReadEntries()
        {
            using (var context = new PhoneBookContext())
            {
                var query = context.PhoneBooks.ToList();

                if (query.Count == 0)
                {
                    Console.WriteLine("No entries found.");
                }
                else
                {
                    Console.WriteLine("Phonebook Entries:");
                    var table = new Table();
                    table.AddColumn("ID");
                    table.AddColumn("Name");
                    table.AddColumn("Email");
                    table.AddColumn("Phone Number");
                    table.AddColumn("Category");

                    foreach (var entry in query)
                    {
                        table.AddRow(entry.Id.ToString(), entry.Name, entry.Email, entry.PhoneNumber, entry.Category);
                    }

                    AnsiConsole.Write(table);
                }
            }
        }

        public static void DeleteEntry()
        {
            using (var context = new PhoneBookContext())
            {
                AnsiConsole.MarkupLine("[blue]Delete an entry[/]");
                ReadEntries();
                Console.WriteLine("Enter the ID of the entry to delete:");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var entry = context.PhoneBooks.Find(id);
                    if (entry != null)
                    {
                        context.PhoneBooks.Remove(entry);
                        context.SaveChanges();
                        Console.WriteLine("Entry deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Entry not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID format.");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public static void UpdateEntry()
        {
            using (var context = new PhoneBookContext())
            {
                AnsiConsole.MarkupLine("[blue]Update an entry[/]");
                ReadEntries();

                Console.WriteLine("Enter the ID of the entry to update:");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var entry = context.PhoneBooks.Find(id);
                    if (entry != null)
                    {
                        Console.WriteLine("Enter new Name (leave blank to keep current):");
                        string name = Console.ReadLine();
                        string email = EnterEmail();
                        string phoneNumber = EnterPhoneNumber();
                        if (!string.IsNullOrWhiteSpace(name)) entry.Name = name;
                        if (!string.IsNullOrWhiteSpace(email)) entry.Email = email;
                        if (!string.IsNullOrWhiteSpace(phoneNumber)) entry.PhoneNumber = phoneNumber;
                        Console.WriteLine("Enter new Category (leave blank to keep current):");
                        string category = Console.ReadLine();
                        context.SaveChanges();
                        Console.WriteLine("Entry updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Entry not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID format.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static bool IsDigit(string input)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsPhoneNumber(string number)
        {
            // Please note that this specifically checks for UK phone numbers (they start with 07)
            if (string.IsNullOrWhiteSpace(number)) return false;

            number = number.Trim();

            if (number.Length != 11) return false;

            if (!number.StartsWith("07")) return false;

            return IsDigit(number);
        }

        private static string EnterPhoneNumber()
        {
            Console.WriteLine("Please enter a UK phone number");
            Console.WriteLine("UK Phone numbers have an 07 prefix and are 11 digits long");
            string phoneNumber = Console.ReadLine();
            while (!IsPhoneNumber(phoneNumber))
            {
                Console.WriteLine("Invalid phone number format. Please enter a valid UK phone number:");
                phoneNumber = Console.ReadLine();
            }

            return phoneNumber;
        }

        private static bool IsEmail(string email)
        {
            // Some email domains don't contain a dot (.), so I've left this out for now
            return email.Length >= 4 && email.Contains("@");
        }

        private static string EnterEmail()
        {
            Console.WriteLine("Please enter an email address");
            Console.WriteLine("A valid email address must contain atleast 4 characters and an @ symbol");
            string email = Console.ReadLine();
            while (!IsEmail(email))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address:");
                email = Console.ReadLine();
            }

            return email;
        }

        private static void SendEmail()
        {
            ReadEntries();
            Console.WriteLine("Enter an ID to send an email to that contact:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (var context = new PhoneBookContext())
                {
                    var entry = context.PhoneBooks.Find(id);
                    if (entry != null)
                    {
                        Console.WriteLine($"Composing email to {entry.Name} at {entry.Email}");
                        Console.WriteLine("Enter subject:");
                        string subject = Console.ReadLine();
                        Console.WriteLine("Enter email body:");
                        string body = Console.ReadLine();
                        string email = entry.Email;
                        Email.SendEmail(email, subject, body);
                        Console.WriteLine("Email process completed. Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Entry not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        private static void SendSMS()
        {
            ReadEntries();
            Console.WriteLine("Enter an ID to send an SMS to that contact:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (var context = new PhoneBookContext())
                {
                    var entry = context.PhoneBooks.Find(id);
                    if (entry != null)
                    {
                        Console.WriteLine($"Composing SMS to {entry.Name} at {entry.PhoneNumber}");
                        Console.WriteLine("Enter SMS body:");
                        string body = Console.ReadLine();
                        string phoneNumber = entry.PhoneNumber;
                        SMS.SendSms(phoneNumber, body);
                        Console.WriteLine("SMS process completed. Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Entry not found.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}