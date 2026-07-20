using Phonebook.Models;
using System;

namespace Phonebook
{
    public class UserInput
    {
        private readonly ContactService _contactService;
        private readonly EmailService _emailService;
        private readonly SmsService _smsService;
        private SmtpConfig? _cachedSmtpConfig;
        private SmsConfig? _cachedSmsConfig;

        public UserInput(ContactService contactService)
        {
            _contactService = contactService;
            _emailService = new EmailService();
            _smsService = new SmsService();
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
            choices.Add("Send e-mail message to this contact.");
            choices.Add("Add/Edit e-mail address.");
            choices.Add("Send SMS message to this contact.");
            TableVisualizer.ShowChoices(choices, "ACTION");

            string? inp = Console.ReadLine();
            while (!Validator.IsValidChoice(inp, 5))
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
                case (3):
                    SendEmailInput(contact);
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    return;
                case (4):
                    AddOrUpdateEmailInput(contact);
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    return;
                case (5):
                    SendSmsInput(contact);
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("\nPress any key to go back to the menu.");
                    Console.ReadKey();
                    return;
            }
        }

        private void SendSmsInput(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.PhoneNumber) || !Validator.IsValidNumber(contact.PhoneNumber))
            {
                Console.WriteLine("\nThis contact does not have a valid phone number for SMS.");
                return;
            }

            Console.WriteLine($"\nSEND SMS TO {contact.Name} ({contact.PhoneNumber})");

            Console.Write("\nEnter SMS message body:\n> ");
            string? message = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("\nSMS message body cannot be empty.");
                return;
            }

            SmsConfig config = GetSmsConfigPrompt();

            Console.WriteLine("\nSending SMS...");
            _smsService.SendSms(contact.PhoneNumber, message, config);
        }

        private SmsConfig GetSmsConfigPrompt()
        {
            if (_cachedSmsConfig != null)
            {
                string modeStr = _cachedSmsConfig.IsSimulation ? "Simulation Mode" : $"API Mode via {_cachedSmsConfig.FromPhoneNumber}";
                Console.Write($"\nUse previous SMS settings ({modeStr})? (y/n):\n> ");
                string? ans = Console.ReadLine()?.Trim().ToLower();
                if (ans == "y" || ans == "yes")
                {
                    return _cachedSmsConfig;
                }
            }

            var config = new SmsConfig();
            Console.WriteLine("\nSMS CONFIGURATION");
            Console.Write("Run in Simulation Mode? (y/n, default: y):\n> ");
            string? simAns = Console.ReadLine()?.Trim().ToLower();

            if (simAns == "n" || simAns == "no")
            {
                config.IsSimulation = false;
                Console.Write("Enter Account SID:\n> ");
                config.AccountSid = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Enter Auth Token:\n> ");
                config.AuthToken = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Enter From Phone Number (e.g. +12123456789):\n> ");
                config.FromPhoneNumber = Console.ReadLine()?.Trim() ?? "";
            }
            else
            {
                config.IsSimulation = true;
            }

            _cachedSmsConfig = config;
            return config;
        }

        private void SendEmailInput(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Email))
            {
                Console.WriteLine("\nThis contact does not have an e-mail address.");
                Console.Write("Would you like to add an e-mail address now? (y/n):\n> ");
                string? ans = Console.ReadLine()?.Trim().ToLower();
                if (ans == "y" || ans == "yes")
                {
                    bool updated = AddOrUpdateEmailInput(contact);
                    if (!updated || string.IsNullOrWhiteSpace(contact.Email))
                    {
                        Console.WriteLine("\nCannot send email without a valid recipient email address.");
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            Console.WriteLine($"\nSEND E-MAIL TO {contact.Name} ({contact.Email})");

            Console.Write("\nEnter email subject:\n> ");
            string? subject = Console.ReadLine()?.Trim() ?? "";

            Console.Write("\nEnter email message body:\n> ");
            string? body = Console.ReadLine()?.Trim() ?? "";

            SmtpConfig config = GetSmtpConfigPrompt();

            Console.WriteLine("\nSending email...");
            _emailService.SendEmail(contact.Email!, subject, body, config);
        }

        private bool AddOrUpdateEmailInput(Contact contact)
        {
            Console.WriteLine($"\nADD / EDIT E-MAIL FOR {contact.Name}");
            Console.Write("Enter email address (or 0 to exit):\n> ");
            string? inpEmail = Console.ReadLine()?.Trim();

            if (inpEmail == "0")
                return false;

            while (!Validator.IsValidEmail(inpEmail))
            {
                Console.WriteLine("\nPlease enter a valid email address (e.g., name@example.com) or 0 to exit.");
                Console.Write("> ");
                inpEmail = Console.ReadLine()?.Trim();
                if (inpEmail == "0")
                    return false;
            }

            bool success = _contactService.UpdateContactEmail(contact.Id, inpEmail!);
            if (success)
            {
                contact.Email = inpEmail;
                Console.WriteLine($"\nE-mail address updated to '{inpEmail}' successfully!");
                return true;
            }
            else
            {
                Console.WriteLine("\nFailed to update e-mail address.");
                return false;
            }
        }

        private SmtpConfig GetSmtpConfigPrompt()
        {
            if (_cachedSmtpConfig != null)
            {
                Console.Write($"\nUse previous SMTP settings ({_cachedSmtpConfig.SenderEmail} via {_cachedSmtpConfig.Host})? (y/n):\n> ");
                string? ans = Console.ReadLine()?.Trim().ToLower();
                if (ans == "y" || ans == "yes")
                {
                    return _cachedSmtpConfig;
                }
            }

            var config = new SmtpConfig();
            Console.WriteLine("\nSMTP CONFIGURATION");

            Console.Write("Enter SMTP Host (default: smtp.gmail.com):\n> ");
            string? host = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(host))
            {
                config.Host = host;
            }

            Console.Write("Enter SMTP Port (default: 587):\n> ");
            string? portStr = Console.ReadLine()?.Trim();
            if (int.TryParse(portStr, out int port))
            {
                config.Port = port;
            }

            Console.Write("Enter Sender Email:\n> ");
            string? senderEmail = Console.ReadLine()?.Trim();
            while (!Validator.IsValidEmail(senderEmail))
            {
                Console.WriteLine("Please enter a valid sender email address (e.g. yourname@gmail.com):");
                Console.Write("> ");
                senderEmail = Console.ReadLine()?.Trim();
            }
            config.SenderEmail = senderEmail!;

            Console.Write("Enter Sender Password / App Password:\n> ");
            string? password = Console.ReadLine() ?? "";
            config.Password = password;

            Console.Write("Enable SSL/TLS? (y/n, default: y):\n> ");
            string? sslAns = Console.ReadLine()?.Trim().ToLower();
            if (sslAns == "n" || sslAns == "no")
            {
                config.EnableSsl = false;
            }

            _cachedSmtpConfig = config;
            return config;
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
