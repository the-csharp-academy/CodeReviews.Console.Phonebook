using Phonebook.Models;
using System;

namespace Phonebook
{
    public class UserInput
    {
        private readonly ContactService _contactService;
        public UserInput(ContactService contactService)
        {
            _contactService = contactService;
            GetMenuInput();
        }

        public void GetMenuInput()
        {
            Console.Clear();
            List<string> choices = new List<string>();
            choices.Add("Exit.");
            choices.Add("Get category list.");
            choices.Add("Search contact by phone number.");
            choices.Add("Search contact by name.");
            choices.Add("See all contact.");
            choices.Add("Add new contact.");
            TableVisualizer.ShowChoices(choices, "MENU");

            string? inp = Console.ReadLine();
            while (!Validator.IsValidChoice(inp, 5))
            {
                Console.Write("\nPlease enter a valid number!\nTry again:\n> ");
                inp = Console.ReadLine();
            }
            int choice = int.Parse(inp!);

            switch (choice)
            {
                case 0:
                    Console.WriteLine("\nGood bye!");
                    break;
                case 1:
                    GetCategoryInput();
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    GetMenuInput();
                    break;
                case 2:
                    GetNumberInput();
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    GetMenuInput();
                    break;
                case 3:
                    GetNameInput();
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    GetMenuInput();
                    break;
                case 4:
                    GetAllContactInput();
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    GetMenuInput();
                    break;
                case 5:
                    AddContactInput();
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    GetMenuInput();
                    break;
                default:
                    break;
            }
        }

        private void GetCategoryInput()
        {
            var categories = _contactService.GetCategories();
            if (categories == null || categories.Count == 0)
            {
                Console.WriteLine("\nNo categories available or database error occurred.");
                return;
            }

            TableVisualizer.ShowSingleColumn(categories, "Category name", "Category List");

            Console.WriteLine("\nChoose a category to see all contacts or go back to menu by typing 0:");

            string? category;
            while (true)
            {
                Console.Write("> ");
                category = Console.ReadLine()?.Trim() ?? "";

                if (category == "0")
                    return;

                if (Validator.IsValidString(category) && categories.Any(x => string.Equals(x, category, StringComparison.OrdinalIgnoreCase)))
                {
                    break;
                }

                Console.WriteLine("\nInvalid category. Please choose a category from the list above or 0 to exit.");
            }
            GetContactFromCategoryInput(category);
        }

        private void GetContactFromCategoryInput(string category)
        {
            var contacts = _contactService.GetContactByCategory(category);
            if (contacts == null || contacts.Count == 0)
            {
                Console.WriteLine("\nNo contacts found for category: " + category);
                return;
            }

            TableVisualizer.ShowTable(contacts, "Contact list of category " + category);

            var contact = ChooseContactInput(contacts);
            if (contact != null)
            {
                GetContactInput(contact);
            }
        }

        private void GetNumberInput()
        {
            Console.WriteLine("Enter a phone number to search contact or go back to menu by typing 0.");

            string? phoneNumber;
            while (true)
            {
                Console.Write("> ");
                phoneNumber = Console.ReadLine()?.Trim() ?? "";

                if (phoneNumber == "0")
                    return;

                if (Validator.IsValidNumber(phoneNumber))
                {
                    break;
                }

                Console.WriteLine("\nInvalid number.");
            }

            var contact = _contactService.GetContactByNumber(phoneNumber);
            GetContactInput(contact);
        }

        private void GetNameInput()
        {
            Console.WriteLine("Enter a name to search contact or go back to menu by typing 0.");

            string? name;
            while (true)
            {
                Console.Write("> ");
                name = Console.ReadLine()?.Trim() ?? "";

                if (name == "0")
                    return;

                if (Validator.IsValidString(name))
                {
                    break;
                }

                Console.WriteLine("\nInvalid name.");
            }

            var contacts = _contactService.GetContactByName(name);
            if (contacts == null || contacts.Count == 0)
            {
                Console.WriteLine("\nNo contacts found matching: " + name);
                return;
            }

            TableVisualizer.ShowTable(contacts, "Contact list of name " + name);

            var contact = ChooseContactInput(contacts);
            if (contact != null)
            {
                GetContactInput(contact);
            }
        }

        private void GetAllContactInput()
        {
            var contacts = _contactService.GetAll();
            if (contacts == null || contacts.Count == 0)
            {
                Console.WriteLine("\nNo contacts found or database error occurred!");
                return;
            }

            TableVisualizer.ShowTable(contacts, "Contact list");

            var contact = ChooseContactInput(contacts);
            if (contact != null)
            {
                GetContactInput(contact);
            }
        }

        private void GetContactInput(Contact? contact)
        {
            if (contact == null)
            {
                Console.WriteLine("\nNo contact found!");
                return;
            }

            List<Contact> contacts = new();
            contacts.Add(contact);
            TableVisualizer.ShowTable(contacts, "Contact info");

            Console.WriteLine("\n\n\n");

            List<string> choices = new();
            choices.Add("Exit.");
            choices.Add("Edit contact information.");
            choices.Add("Delete this contact.");
            TableVisualizer.ShowChoices(choices, "ACTION");

            string? inp = Console.ReadLine();
            while (!Validator.IsValidChoice(inp, 2))
            {
                Console.Write("\nPlease enter a valid number!\n> ");
                inp = Console.ReadLine();
            }
            int choice = int.Parse(inp!);

            switch (choice)
            {
                case (0):
                    return;
                case (1):
                    UpdateContactInput(contact.Id);
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    return;
                case (2):
                    DeleteContactInput(contact.Id);
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    return;
            }
        }

        private void AddContactInput()
        {
            Console.Clear();
            Console.WriteLine("\tADD NEW CONTACT");

            Console.Write("\nEnter name:\n> ");
            string? inpName = Console.ReadLine();
            while(!Validator.IsValidString(inpName))
            {
                Console.WriteLine("\nPlease enter a valid name or 0 to exit.");
                Console.Write("> ");
                inpName = Console.ReadLine();
                if (inpName == "0")
                    return;
            }

            Console.Write("\nEnter phone number:\n> ");
            string? inpNumber = Console.ReadLine();
            while (!Validator.IsValidNumber(inpNumber))
            {
                Console.WriteLine("\nPlease enter a valid phone number or 0 to exit (eg: +12123456789).");
                Console.Write("> ");
                inpNumber = Console.ReadLine();
                if (inpNumber == "0")
                    return;
            }

            Console.Write("\nEnter email:\n> ");
            string? inpEmail= Console.ReadLine();
            while (!Validator.IsValidEmail(inpEmail))
            {
                Console.WriteLine("\nPlease enter a valid email or 0 to exit (eg: name@example.com).");
                Console.Write("> ");
                inpEmail = Console.ReadLine();
                if (inpEmail== "0")
                    return;
            }

            Console.Write("\nEnter category:\n> ");
            string? inpCategory = Console.ReadLine();
            while (!Validator.IsValidString(inpCategory))
            {
                Console.WriteLine("\nPlease enter a valid category or 0 to exit.");
                Console.Write("> ");
                inpCategory = Console.ReadLine();
                if (inpCategory == "0")
                    return;
            }

            bool success = _contactService.AddContact(inpName!, inpNumber!, inpEmail, inpCategory);
            if (success)
            {
                Console.WriteLine("\nNew contact added successfully!");
            }
            else
            {
                Console.WriteLine("\nOperation failed. Please check your database connection or try again later!");
            }
        }

        private void DeleteContactInput(int contactId)
        {
            bool success = _contactService.DeleteContact(contactId);
            if(success)
            {
                Console.WriteLine("Contact deleted successfully!");
            }
            else
            {
                Console.WriteLine("Operation failed. Please check your database connection or try again later!");
            }
        }

        private void UpdateContactInput(int contactId)
        {
            Console.WriteLine("\n\n\n\tUPDATE CONTACT INFO");

            Console.Write("\nEnter name:\n> ");
            string? inpName = Console.ReadLine();
            while (!Validator.IsValidString(inpName))
            {
                Console.WriteLine("\nPlease enter a valid name or 0 to exit.");
                Console.Write("> ");
                inpName = Console.ReadLine();
                if (inpName == "0")
                    return;
            }

            Console.Write("\nEnter phone number:\n> ");
            string? inpNumber = Console.ReadLine();
            while (!Validator.IsValidNumber(inpNumber))
            {
                Console.WriteLine("\nPlease enter a valid phone number or 0 to exit (eg: +12123456789).");
                Console.Write("> ");
                inpNumber = Console.ReadLine();
                if (inpNumber == "0")
                    return;
            }

            Console.Write("\nEnter email:\n> ");
            string? inpEmail = Console.ReadLine();
            while (!Validator.IsValidEmail(inpEmail))
            {
                Console.WriteLine("\nPlease enter a valid email or 0 to exit (eg: name@example.com).");
                Console.Write("> ");
                inpEmail = Console.ReadLine();
                if (inpEmail == "0")
                    return;
            }

            Console.Write("\nEnter category:\n> ");
            string? inpCategory = Console.ReadLine();
            while (!Validator.IsValidString(inpCategory))
            {
                Console.WriteLine("\nPlease enter a valid category or 0 to exit.");
                Console.Write("> ");
                inpCategory = Console.ReadLine();
                if (inpCategory == "0")
                    return;
            }

            bool success = _contactService.UpdateContactInfo(contactId, inpName!, inpNumber!,inpEmail, inpCategory);
            if (success)
            {
                Console.WriteLine("Contact updated successfully!");
            }
            else
            {
                Console.WriteLine("Operation failed. Please check your database connection or try again later!");
            }
        }

        private Contact? ChooseContactInput(List<Contact> contacts)
        {
            if (contacts == null || contacts.Count == 0)
            {
                return null;
            }

            string? inp;
            int contactId = -1;

            Console.WriteLine("\nChoose a contact by ID or go back to menu by typing 0:\t");

            while (true)
            {
                Console.Write("Your choice:\t");
                inp = Console.ReadLine()?.Trim() ?? "";

                if (inp == "0")
                    return null;

                if (Validator.IsValidString(inp) && Int32.TryParse(inp, out contactId) && contacts.Any(x => x.Id == contactId))
                    break;

                Console.WriteLine("\nInvalid contact id. Please choose an id from the list above or 0 to exit.");
            }
            return contacts.FirstOrDefault(x => x.Id == contactId);
        }
    }
}
