using Phonebook.Model;

namespace Phonebook.Controller;

internal class ContactController
{
    static DatabaseManager dbmanager = new DatabaseManager();
    static Helpers helpers = new Helpers();
    static List<Contact> contacts = new List<Contact>();

    internal static void ViewContacts()
    {
        contacts.Clear();

        Console.Clear();
        Console.WriteLine("---View all Contacts---");

        contacts = dbmanager.ViewContacts();

        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts available");
        }
        else
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Name: {contact.Name}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"E-mail: {contact.EmailAddress}");
                Console.WriteLine();
            }
        }

        Console.WriteLine("Press enter to go back to the main menu.");
        Console.ReadLine();
    }

    internal static void NewContact()
    {
        Console.Clear();
        Console.WriteLine("---Add New Contact---");
        Console.WriteLine("Insert new contact name (Insert 0 to return to main menu):");
        string name = helpers.CheckName();

        if (name == "0") return;

        Console.WriteLine("Insert new contact phone number (ie.+15166024355)(Insert 0 to return to main menu): ");
        string number = helpers.CheckPhoneNumber();

        if(number == "0") return;

        Console.WriteLine("Insert new contact email address (Insert 0 to return to main menu):");
        string email = helpers.CheckEmail();

        if(email == "0") return;

        if(dbmanager.AddContact(name, number, email))
            Console.WriteLine("\nNew Contact has been added in the Phonebook!");
        else
            Console.WriteLine("\nContact added unsuccessful");

        Console.WriteLine("Would you like to add another contact?: (y/n)");
        string input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input) && input.ToLower() == "y")
            NewContact();
    }

    internal static void DeleteContact()
    {
        contacts.Clear();

        Console.Clear();
        Console.WriteLine("---Delete Contact---");

        Console.WriteLine("Insert the contact name (Insert 0 to return to main menu): ");
        string name = helpers.CheckName();

        if (name == "0") return;

        contacts = dbmanager.FindContact(name);

        if (contacts.Count == 0)
        {
            Console.WriteLine("Contact not found\n");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("---Delete Contact---");
            Console.WriteLine($"{contacts.Count} found\n");

            foreach (var contact in contacts)
            {
                Console.WriteLine($"ID: {contact.Id}");
                Console.WriteLine($"Name: {contact.Name}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"E-mail: {contact.EmailAddress}");
                Console.WriteLine();
            }

            Console.WriteLine("Insert their ID to delete their contact (Insert 0 to return to main menu): ");
            int id = helpers.CheckNumber();

            if (id == 0) return;

            List<Contact> foundContact = new List<Contact>();
            foundContact = dbmanager.FindContactByIdAndName(id, name);


            if (foundContact.Count == 0)
            {
                Console.WriteLine("ID doesn't match to any of the contact name.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("---Delete Contact---");
                foreach (var contact in foundContact)
                {
                    Console.WriteLine($"ID: {contact.Id}");
                    Console.WriteLine($"Name: {contact.Name}");
                    Console.WriteLine($"Phone: {contact.PhoneNumber}");
                    Console.WriteLine($"E-mail: {contact.EmailAddress}");
                    Console.WriteLine();
                }

                Console.WriteLine("Are you sure you want to delete this contact? (y/n)");
                string confirmation = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(confirmation) && confirmation.Trim().ToLower() == "y")
                {
                    if (dbmanager.DeleteById(id))
                        Console.WriteLine("Contact deleted sucessfully!");
                    else
                        Console.WriteLine("Delete contact unsuccessful");
                }
                else
                    Console.WriteLine("Delete contact cancelled");
            }
        }

        Console.WriteLine("\nWould you like to delete another contact?: (Y/N)");
        string input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input) && input.ToLower() == "y")
            DeleteContact();

    }

    internal static void UpdateContact()
    {
        Console.Clear();
        Console.WriteLine("---Update Contact---");

        Console.WriteLine("Insert the contact name (Insert 0 to return to main menu): ");
        string name = helpers.CheckName();

        if (name == "0") return;

        contacts = dbmanager.FindContact(name);

        if (contacts.Count == 0)
        {
            Console.WriteLine("Contact not found\n");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("---Update Contact---");
            Console.WriteLine($"{contacts.Count} found\n");

            foreach (var contact in contacts)
            {
                Console.WriteLine($"ID: {contact.Id}");
                Console.WriteLine($"Name: {contact.Name}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"E-mail: {contact.EmailAddress}");
                Console.WriteLine();
            }

            Console.WriteLine("Insert their ID to update their contact (Insert 0 to return to main menu): ");
            int id = helpers.CheckNumber();

            if (id == 0) return;

            List<Contact> foundContact = new List<Contact>();
            foundContact = dbmanager.FindContactByIdAndName(id, name);

            if (foundContact.Count == 0)
            {
                Console.WriteLine("ID doesn't match to any of the contact name.");
            }
            else
            {
                var contact = foundContact[0];
                bool isCompleted = false;

                while (!isCompleted)
                {
                    Console.Clear();
                    Console.WriteLine("---Update Contact---");
                    Console.WriteLine($"Name: {contact.Name}");
                    Console.WriteLine($"Phone: {contact.PhoneNumber}");
                    Console.WriteLine($"E-mail: {contact.EmailAddress}");
                    Console.WriteLine();
                    Console.WriteLine("1 - Update name.");
                    Console.WriteLine("2 - Update phone number.");
                    Console.WriteLine("3 - Update email address.");
                    Console.WriteLine("0 - Complete changes");

                    string updateInput = Console.ReadLine();

                    switch (updateInput)
                    {
                        case "0":
                            isCompleted = true;
                            break;
                        case "1":
                            Console.WriteLine("\nInsert new contact name (Insert 0 to return to keep): ");
                            string newName = helpers.CheckName();
                            if(newName != "0")
                                contact.Name = newName;
                            break;
                        case "2":
                            Console.WriteLine("\nInsert new contact phone number (00-0000-0000) (Insert 0 to return to keep): ");
                            string number = helpers.CheckPhoneNumber();
                            if (number != "0")
                                contact.PhoneNumber = number;
                            break;
                        case "3":
                            Console.WriteLine("Insert new contact email address (Insert 0 to return to keep):");
                            string email = helpers.CheckEmail();
                            if (email != "0")
                                contact.EmailAddress = email;
                            break;
                    }

                }

                if (dbmanager.UpdateById(id, contact.Name, contact.PhoneNumber, contact.EmailAddress))
                    Console.WriteLine("Contact update sucessful!");
                else
                    Console.WriteLine("Contact update unsuccessful");

            }
        }

        Console.WriteLine("\nWould you like to update another contact?: (Y/N)");
        string input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input) && input.ToLower() == "y")
            UpdateContact();
    }

    internal static void FindContact()
    {
        contacts.Clear();

        Console.Clear();
        Console.WriteLine("---Find Contact---");
        Console.WriteLine("Insert the contact name: ");
        string input = helpers.CheckName();

        if (input == "0") return;

        contacts = dbmanager.FindContact(input);

        if (contacts.Count == 0)
        {
            Console.WriteLine("Contact not found\n");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("---Find Contact---");
            Console.WriteLine($"{contacts.Count} found\n");

            foreach (var contact in contacts)
            {
                Console.WriteLine($"Name: {contact.Name}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"E-mail: {contact.EmailAddress}");
                Console.WriteLine();
            }
        }

        Console.WriteLine("Press enter to go back to the main menu.");
        Console.ReadLine();
    }
}
