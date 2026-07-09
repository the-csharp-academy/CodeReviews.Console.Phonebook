using Microsoft.Extensions.Configuration;
using PhoneBook.Controller;
using PhoneBook.Menus;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Service;

internal class ContacService
{
    private readonly EmailService emailService;

    public ContacService(IConfiguration configuration)
    {
        emailService = new EmailService(configuration);
    }


    internal void InsertContact()
    {
        Contact contact = new Contact();
        contact.Name = ValidateInput.ValidateName("Contact's name: ");
        contact.PhoneNumber = ValidateInput.ValidatePhone("\"Enter your phone number in international format [#6C91C2](e.g. +573001234567)[/]:");
        contact.Email = ValidateInput.ValidateEmail("Contact's email [#6C91C2](e.g youremail@gmail.com)[/]");
        contact.CategoryId = CategoryService.GetCategoryOptionInput().CategoryId;

        if (ContactController.AddContact(contact))
        {
            if (!emailService.IsConfigured())
            {
                AnsiConsole.MarkupLine(
                "[#ACD8AA]Email service is not configured. The contact was created, but no email was sent.[/]");

            }
            else
            {
                emailService.SendWelcomeEmail("PhoneBook", contact);
                AnsiConsole.MarkupLine("[#6EEB83]Contact created successfully.[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed to create contact.[/]");
        }

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();
    }

    internal static void UpdateContact()
    {
        Contact contact = GetContactNameOptionInput();

        if (contact == null)
        {
            AnsiConsole.MarkupLine("[#FF934F]There are no contacts[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        }

        contact.Name = AnsiConsole.Confirm("Update Name: ")
            ? ValidateInput.ValidateName("Contact's name: ")
            : contact.Name;

        contact.PhoneNumber = AnsiConsole.Confirm("Update Phone Number:")
            ? ValidateInput.ValidatePhone("Contac's phone number [#6C91C2](e.g +57 123 32156)[/]")
            : contact.PhoneNumber;

        contact.Email = AnsiConsole.Confirm("Update email")
            ? ValidateInput.ValidateEmail("Contact's email")
            : contact.Email;

        contact.Category = AnsiConsole.Confirm("Update Category")
            ? CategoryService.GetCategoryOptionInput()
            : contact.Category;

        if (ContactController.UpdateContact(contact)) 
        {
            AnsiConsole.MarkupLine("[#6EEB83]Contact update successfully.[/]");
        }
        else 
        {
            AnsiConsole.MarkupLine("[red]Failed to update contact.[/]");
        }

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();
    }

    internal static void ViewAllContacts()
    {
        var contacts = ContactController.GetAllContacts();

        if (contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[#FF934F]There are no contacts[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        }

        UserInterface.ShowContactsTable(contacts);
    }

    internal static void ViewContact()
    {
        var contact = GetContactNameOptionInput();

        if (contact == null)
        {
            AnsiConsole.MarkupLine("[#FF934F]There are no contacts[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        }

        UserInterface.ViewContact(contact);
    }

    internal static void DeleteContact()
    {
        var contact = GetContactNameOptionInput();

        if (contact == null)
        {
            AnsiConsole.MarkupLine("[#FF934F]There are no contacts[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        }

        if (ContactController.DeleteContact(contact))
        {
            AnsiConsole.MarkupLine("[#6EEB83]Contact delete successfully.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed to delete contact.[/]");
        }
    }

    internal static Contact GetContactNameOptionInput()
    {
        var contacts = ContactController.GetAllContacts();

        if (contacts.Count == 0)
        {
            return null;
        }

        string[] contactsName = contacts.Select(c => c.Name).ToArray();

        var options = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a Contact")
            .AddChoices(contactsName));

        int id = contacts.Single(c => c.Name == options).ContactId;
        Contact contact = ContactController.GetContactById(id);

        return contact;
    }
}