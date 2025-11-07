using kilozdazolik.Phonebook.Controllers;
using kilozdazolik.Phonebook.Models;
using Spectre.Console;

namespace kilozdazolik.Phonebook.Services;

public class ContactService
{
    internal static void GetContacts()
    {
        var contact = GetContactOptionInput();
        UserInterface.ShowContactOptions(contact);
    }

    internal static void AddContact()
    {
        var contact = new Contact();
        
        contact.Name = Helper.GetNameInput();
        contact.Email = Helper.GetEmailInput();
        contact.PhoneNumber = Helper.GetPhoneNumberInput();
        
        ContactController.InsertContact(contact);
    }

    internal static void DeleteContact()
    {
        var contact = GetContactOptionInput();

        if (Helper.ConfirmMessage("Delete", contact.Name))
        {
            ContactController.RemoveContact(contact);
        }
        else
        {
            AnsiConsole.MarkupLine("Deletion canceled.");
        }
        
        AnsiConsole.MarkupLine("[green]Press Any Key to Continue.[/]");
        Console.ReadKey();
    }

    internal static void EditContact()
    {
        var contact = GetContactOptionInput();

        var isEditSelectionRunning = true;
        while (isEditSelectionRunning)
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Edit Contact")
                    .AddChoices("Name", "Email", "Phone number", "Go Back")
            );

            switch (option)
            {
                case "Name":
                    contact.Name = Helper.GetNameInput();
                    break;
                case "Email":
                    contact.Email = Helper.GetEmailInput();
                    break;
                case "Phone number":
                    contact.PhoneNumber = Helper.GetPhoneNumberInput();
                    break;
                case "Go Back":
                    isEditSelectionRunning = false;
                    break;
            }
        }

        ContactController.UpdateContact(contact);
    }
    
    internal static void GetContactsByCategory()
    {
        var category = CategoryService.GetCategoryOptionInput();
        var contacts = ContactController.GetContactsByCategory(category);
        UserInterface.ShowTable(contacts);
    }

    internal static Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetContacts();
        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option =
            AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose Contact").AddChoices(contactsArray));
        var contact = contacts.Single(x => x.Name == option);
        return contact;
    }
}