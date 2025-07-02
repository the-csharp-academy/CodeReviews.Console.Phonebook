using Phone_Book.Controllers;
using Phone_Book.Models;
using Phone_Book.Views;
using Spectre.Console;

namespace Phone_Book.Services;

public class ContactService
{
    internal static void InsertContact()
    {
        var contact = new Contact();

        contact.Name = AnsiConsole.Ask<string>("What is the contact's name?");

        contact.Email = AnsiConsole.Ask<string>(
            "What is the contact's email? (format: example@example.com)"
        );

        while (!Validator.IsValidEmail(contact.Email))
            contact.Email = AnsiConsole.Ask<string>(
                "[Red]Incorrect format. Try again (format: example@example.com)[/]"
            );

        contact.Phone = AnsiConsole.Ask<string>(
            "What is the contact's phone number? (format: 123-456-7890)"
        );

        while (!Validator.IsValidPhone(contact.Phone))
            contact.Phone = AnsiConsole.Ask<string>(
                "[Red]Incorrect format or length. Try again (format: 123-456-7890)[/]"
            );

        contact.CategoryId = CategoryService.GetCategoryByName().CategoryId;

        ContactController.AddContact(contact);
    }

    internal static void UpdateContact()
    {
        var contact = GetContactByName();

        var updateName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to update the contact's name?")
                .AddChoices("Yes", "No")
        );
        if (updateName == "Yes")
            contact.Name = AnsiConsole.Ask<string>("What is the contact's name?");
        else
            contact.Name = contact.Name;

        var updateEmail = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to update the contact's email address?")
                .AddChoices("Yes", "No")
        );
        if (updateEmail == "Yes")
        {
            contact.Email = AnsiConsole.Ask<string>(
                "What is the contact's email? (format: example@example.com)"
            );

            while (!Validator.IsValidEmail(contact.Email))
                contact.Email = AnsiConsole.Ask<string>(
                    "[Red]Incorrect format. Try again (format: example@example.com)[/]"
                );
        }
        else
        {
            contact.Email = contact.Email;
        }

        var updatePhoneNumber = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to update the contact's phone number?")
                .AddChoices("Yes", "No")
        );
        if (updatePhoneNumber == "Yes")
        {
            contact.Phone = AnsiConsole.Ask<string>(
                "What is the contact's phone number? (format: 123-456-7890)"
            );

            while (!Validator.IsValidPhone(contact.Phone))
                contact.Phone = AnsiConsole.Ask<string>(
                    "[Red]Incorrect format or length. Try again (format: 123-456-7890)[/]"
                );
        }
        else
        {
            contact.Phone = contact.Phone;
        }

        var updateCategory = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to update the contact's category?")
                .AddChoices("Yes", "No")
        );
        contact.CategoryId = updateCategory == "Yes" ? CategoryService.GetCategoryByName().CategoryId : contact.CategoryId;

        ContactController.EditContact(contact);
    }

    internal static void DeleteContact()
    {
        var contact = GetContactByName();

        ContactController.DeleteContact(contact);
    }

    internal static void GetContactById()
    {
        var contact = GetContactByName();

        UserInterface.ShowContactTable(contact);
    }

    internal static void GetAllContacts()
    {
        var contacts = ContactController.GetAllContacts();

        UserInterface.ShowAllContactsTable(contacts);
    }

    internal static Contact GetContactByName()
    {
        var contacts = ContactController.GetAllContacts();

        var contactsArray = contacts.Select(c => c.Name).ToArray();
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("Choose an option:").AddChoices(contactsArray)
        );

        var contactId = contacts.Single(c => c.Name == option).Id;
        var contact = ContactController.GetContactById(contactId);

        return contact;
    }

    internal static string GetContactByEmail()
    {
        var contact = GetContactByName();

        return contact.Email;
    }
}
