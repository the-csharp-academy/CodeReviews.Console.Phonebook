using Spectre.Console;

namespace PhoneBook.EntityFramework
{
    internal class ContactService
    {
        internal static void InsertContact()
        {
            var name = AnsiConsole.Ask<string>("Contacts Name: ");

            string email;
            do
            {
                email = AnsiConsole.Ask<string>("Contacts Email (format: example@domain.com): ");
                if (!Validation.IsValidEmail(email))
                    AnsiConsole.MarkupLine("[red]Invalid email. Expected format: example@domain.com[/]");
            } while (!Validation.IsValidEmail(email));

            string phone;
            do
            {
                phone = AnsiConsole.Ask<string>("Contacts Phone Number (format: +447911123456): ");
                if (!Validation.IsValidPhone(phone))
                    AnsiConsole.MarkupLine("[red]Invalid phone. Expected format: +447911123456[/]");
            } while (!Validation.IsValidPhone(phone));

            ContactController.AddContact(name, email, phone);
        }

        internal static void DeleteContact()
        {
            var contact = GetContactOptionInput();
            ContactController.DeleteContact(contact);
        }

        internal static void UpdateContact()
        {
            var contact = GetContactOptionInput();

            contact.Name = AnsiConsole.Ask<string>("Contacts new Name");
            contact.Email = AnsiConsole.Ask<string>("Contacts new Email");
            contact.Phone = AnsiConsole.Ask<string>("Contacts new Phone");


            ContactController.UpdateContact(contact);
        }

        internal static void ViewAllContacts()
        {
            var contacts = ContactController.ViewAllContacts();
            UserInterface.ShowContactTable(contacts);
        }

        internal static void ViewContacts()
        {
            var contact = ContactService.GetContactOptionInput();
            UserInterface.ShowContact(contact);
        }

        static private Contact GetContactOptionInput()
        {
            var contacts = ContactController.ViewAllContacts();
            var contactsArray = contacts.Select(x => x.Name).ToArray();
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose Contact")
                .AddChoices(contactsArray));

            var id = contacts.Single(x => x.Name == option).Id;
            var contact = ContactController.ViewContactById(id);
            //displays selected name by the id it was given by entity framework

            return contact;
            //return contact

        }
    }
}
