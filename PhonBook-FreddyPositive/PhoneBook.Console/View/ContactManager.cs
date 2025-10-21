using PhoneBook.Model;
using PhoneBook.Services;
using Spectre.Console;
using static PhoneBook.Enums;

namespace PhoneBook.View;

internal class ContactManager
{
    internal static PhoneBookDetails GetContactInput()
    {
        PhoneBookDetails contact = new();

        contact.Name = AnsiConsole.Ask<string>("Contact's Name");
        contact.Name = ValidationService.ValidateName(contact.Name);

        contact.EmailAddress = AnsiConsole.Ask<string>("Contact's Email Address");
        contact.EmailAddress = ValidationService.ValidateEmailAddress(contact.EmailAddress);

        contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's Phone Number");
        contact.PhoneNumber = ValidationService.ValidatePhoneNumber(contact.PhoneNumber);

        return contact;
    }

    internal static PhoneBookDTO GetUserInput(string title, bool showAllContacts, int operationId)
    {
        if (showAllContacts)
            PhoneBookService.ViewAllContacts(false);

        var options = AnsiConsole.Prompt(
            new SelectionPrompt<UserInput>()
                .Title(title)
                .AddChoices(
                    UserInput.EmailOnly,
                    UserInput.NameOnly,
                    UserInput.PhoneNumberOnly,
                    UserInput.NameAndPhone,
                    UserInput.EmailAndPhone,
                    UserInput.NameAndEmail,
                    UserInput.NameAndEmailAndPhone,
                    UserInput.ExitToMainMenu
                ));

        if (options == UserInput.ExitToMainMenu)
        {
            MainMenu.PhoneBookUserMenu();
            return null;
        }

        bool isUpdate = operationId == 2;
        return GetContactInput(options, isUpdate);
    }

    private static PhoneBookDTO GetContactInput(UserInput options, bool isUpdate)
    {
        PhoneBookDTO contact = new();

        string Ask(string message) => AnsiConsole.Ask<string>(message);

        string AskAndValidate(string message, Func<string, string> validate)
        {
            string input = AnsiConsole.Ask<string>(message);
            return validate(input);
        }

        string AskField(string fieldName, Func<string, string> validator = null)
        {
            string question = $"Contact's {fieldName}";
            return validator != null ? AskAndValidate(question, validator) : Ask(question);
        }

        switch (options)
        {
            case UserInput.EmailOnly:
                if (isUpdate)
                    contact.DisplayId = AnsiConsole.Ask<int>("Enter the Id of the contact to update");

                contact.EmailAddress = isUpdate
                    ? AskField("Email Address", ValidationService.ValidateEmailAddress)
                    : AskField("Email Address");

                break;

            case UserInput.NameOnly:
                if (isUpdate)
                    contact.DisplayId = AnsiConsole.Ask<int>("Enter the Id of the contact to update");

                contact.Name = isUpdate
                    ? AskField("Name", ValidationService.ValidateName)
                    : AskField("Name");

                break;

            case UserInput.PhoneNumberOnly:
                if (isUpdate)
                    contact.DisplayId = AnsiConsole.Ask<int>("Enter the Id of the contact to update");

                contact.PhoneNumber = isUpdate
                    ? AskField("Phone Number", ValidationService.ValidatePhoneNumber)
                    : AskField("Phone Number");

                break;

            case UserInput.NameAndPhone:
                if (isUpdate)
                    contact.DisplayId = AnsiConsole.Ask<int>("Enter the Id of the contact to update");
                contact.Name = isUpdate

                    ? AskField("Name", ValidationService.ValidateName)
                    : AskField("Name");

                contact.PhoneNumber = isUpdate
                    ? AskField("Phone Number", ValidationService.ValidatePhoneNumber)
                    : AskField("Phone Number");

                break;

            case UserInput.EmailAndPhone:
                if (isUpdate)
                    contact.DisplayId = AnsiConsole.Ask<int>("Enter the Id of the contact to update");

                contact.EmailAddress = isUpdate
                    ? AskField("Email Address", ValidationService.ValidateEmailAddress)
                    : AskField("Email Address");

                contact.PhoneNumber = isUpdate
                    ? AskField("Phone Number", ValidationService.ValidatePhoneNumber)
                    : AskField("Phone Number");

                break;

            case UserInput.NameAndEmail:
                if (isUpdate)
                    contact.DisplayId = AnsiConsole.Ask<int>("Enter the Id of the contact to update");

                contact.Name = isUpdate
                    ? AskField("Name", ValidationService.ValidateName)
                    : AskField("Name");

                contact.EmailAddress = isUpdate
                    ? AskField("Email Address", ValidationService.ValidateEmailAddress)
                    : AskField("Email Address");

                break;

            case UserInput.NameAndEmailAndPhone:
                if (isUpdate)
                    contact.DisplayId = AnsiConsole.Ask<int>("Enter the Id of the contact to update");

                contact.Name = isUpdate
                    ? AskField("Name", ValidationService.ValidateName)
                    : AskField("Name");

                contact.EmailAddress = isUpdate
                    ? AskField("Email Address", ValidationService.ValidateEmailAddress)
                    : AskField("Email Address");

                contact.PhoneNumber = isUpdate
                    ? AskField("Phone Number", ValidationService.ValidatePhoneNumber)
                    : AskField("Phone Number");

                break;

            case UserInput.ExitToMainMenu:
                MainMenu.PhoneBookUserMenu();
                break;
        }
        return contact;
    }

    internal static void DisplayContacts(List<PhoneBookDTO> contacts, bool waitForUser)
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No Search Results For Your Search term");
            PhoneBookService.SearchContact();
        }

        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email Address");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.DisplayId.ToString(), contact.Name, contact.PhoneNumber, contact.EmailAddress);
        }

        AnsiConsole.Write(table);

        if (waitForUser)
        {
            Console.WriteLine("Enter any key to continue");
            Console.ReadLine();
        }
    }

    internal static int GetContactIdToDelete()
    {
        PhoneBookService.ViewAllContacts(false);

        int contactId = AnsiConsole.Ask<int>("Enter the Id of the contact to Delete or enter 0 to return to main menu");

        if (contactId == 0) MainMenu.PhoneBookUserMenu();

        return contactId;
    }
}
