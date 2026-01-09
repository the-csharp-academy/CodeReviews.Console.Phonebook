using Azure;
using PhoneBook.TerrenceLGee.DTOs.ContactDTOs;
using PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;
using PhoneBook.TerrenceLGee.Entities.Enums;
using PhoneBook.TerrenceLGee.Extensions;
using PhoneBook.TerrenceLGee.Extensions.Pagination;
using PhoneBook.TerrenceLGee.PhonebookAppUi.Helpers;
using PhoneBook.TerrenceLGee.PhonebookAppUi.Interfaces;
using PhoneBook.TerrenceLGee.SendEmailModels;
using PhoneBook.TerrenceLGee.Services.Interfaces;
using Spectre.Console;

namespace PhoneBook.TerrenceLGee.PhonebookAppUi;

public class PhonebookUi : IPhonebookUi
{
    private readonly IPhonebookService _phonebookService;
    private readonly IEmailService _emailService;

    public PhonebookUi(
        IPhonebookService phonebookService,
        IEmailService emailService)
    {
        _phonebookService = phonebookService;
        _emailService = emailService;
    }

    public async Task AddContactAsync()
    {
        var wishesToAddContact = AnsiConsole.Confirm("[LightGoldenrod1]Create new contact? [/]");

        if (!wishesToAddContact)
        {
            UiHelpers.PressAnyKeyToContinue("Returning to the previous menu");
            return;
        }

        string? suffix = null;
        string? notes = null;
        string? emailAddress = null;
        string? nickName = null;
        DateOnly? dateOfBirth = null;

        Console.Clear();

        var category = InputHelpers.GetContactCategory();

        var firstName = InputHelpers.GetInput("the contact's first name");
        var lastName = InputHelpers.GetInput("the contact's last name");

        var hasSuffix = AnsiConsole
            .Confirm("[DarkOliveGreen3_1]Does this contact have a suffix? (i.e Jr., Sr. etc? )[/]");

        if (hasSuffix)
        {
            suffix = InputHelpers.GetInput("the contact's suffix");
        }

        var hasNickName = AnsiConsole
            .Confirm("[Khaki1]Do you wish to enter a nickname for this contact? [/]");

        if (hasNickName)
        {
            nickName = InputHelpers.GetInput("the contact's nickname ");
        }

        var hasEmailAddress = AnsiConsole
            .Confirm("[DarkOliveGreen3_1]Would you like to enter the contact's email address [/]");

        if (hasEmailAddress)
        {
            emailAddress = InputHelpers.GetValidEmailAddress();
        }

        var numberOfPhoneNumbers = AnsiConsole
            .Ask<int>("[DarkSeaGreen3]Enter the total number of phone numbers that you will enter for this contact: [/]");

        Console.Clear();

        var phoneNumbers = InputHelpers.CreatePhoneNumbers(numberOfPhoneNumbers);

        var hasDateOfBirth = AnsiConsole
            .Confirm("[DeepSkyBlue2]Would you like to enter the date of birth for this contact? [/]");

        if (hasDateOfBirth)
        {
            dateOfBirth = InputHelpers.GetBirthDate();
        }

        var hasNotes = AnsiConsole
            .Confirm("[LightCyan3]Would you like to enter any notes about this contact? [/]");

        if (hasNotes)
        {
            notes = InputHelpers.GetInput("notes about this contact (limit 300 characters) ");
            while (notes.Length > 300)
            {
                UiHelpers.PressAnyKeyToContinueError("Text for the note is too long, must be less than 300 characters");
                notes = InputHelpers.GetInput("notes about this contact (limit 300 characters) ");
            }
        }

        var contact = new CreateContactDto
        {
            FirstName = firstName,
            LastName = lastName,
            Email = emailAddress,
            PhoneNumbers = phoneNumbers,
            Category = category,
            Notes = notes,
            Suffix = suffix,
            DateOfBirth = dateOfBirth,
            NickName = nickName
        };

        Console.Clear();
        wishesToAddContact = AnsiConsole.Confirm("[LightGoldenrod1]Are you sure you wish to add this new contact? [/]");

        if (!wishesToAddContact)
        {
            UiHelpers
                .PressAnyKeyToContinue("Contact not added\nReturning to the main menu");
            return;
        }

        if (! await _phonebookService.AddContactAsync(contact))
        {
            UiHelpers
                .PressAnyKeyToContinueError("Unable to add your new contact at this time\n" +
                "Returning to the previous menu");
            return;
        }

        UiHelpers.PressAnyKeyToContinue("New contact added successfully");
    }

    public async Task UpdateContactAsync()
    {
        var wishesToUpdateContact = AnsiConsole.Confirm("[LightGoldenrod1]Update a contact? [/]");
        Console.Clear();

        if (!wishesToUpdateContact)
        {
            UiHelpers
                .PressAnyKeyToContinue("Returning to the previous menu");
            return;
        }

        if (! await ViewContactsAsync("updating", "update"))
        {
            return;
        }

        var id = AnsiConsole
            .Ask<int>("[CadetBlue]Enter the id of the contact to update or 0 to return to the previous menu: [/]");

        if (id == 0)
        {
            UiHelpers
                .PressAnyKeyToContinue("Returning to the previous menu");
            return;
        }

        var contactToUpdate = await _phonebookService.GetContactAsync(id);

        if (contactToUpdate is null)
        {
            UiHelpers
                .PressAnyKeyToContinueError("Unable to update contact at this time\n" +
                "Returning to the previous menu");
            return;
        }

        Console.Clear();

        var propertiesToUpdate = AnsiConsole.Prompt(
            new MultiSelectionPrompt<UpdateProperties>()
            .Title("[Aquamarine1]Select all that you wish to update for this contact:[/]")
            .AddChoices(Enum.GetValues<UpdateProperties>())
            .UseConverter(choice => choice.GetDisplayName()));

        Console.Clear();

        var updatedContact = ContactUpdateHelpers.UpdateContact(contactToUpdate, propertiesToUpdate);

        Console.Clear();

        wishesToUpdateContact = AnsiConsole.Confirm("[LightGoldenrod1]Are you sure you wish to update this contact? [/]");

        if (!wishesToUpdateContact)
        {
            UiHelpers
                .PressAnyKeyToContinue("Returning to the previous menu");
            return;
        }

        if (! await _phonebookService.UpdateContactAsync(updatedContact))
        {
            UiHelpers
                .PressAnyKeyToContinueError("Unable to update the contact at this time\n" +
                "Returning to the previous menu");
            return;
        }

        UiHelpers
            .PressAnyKeyToContinue("Contact updated sucessfully!");
    }

    public async Task DeleteContactAsync()
    {
        var wishesToDeleteContact = AnsiConsole.Confirm("[LightGoldenrod1]Delete a contact?[/]");

        if (!wishesToDeleteContact)
        {
            UiHelpers
                .PressAnyKeyToContinue("Returning to the previous menu");
            return;
        }

        if (!await ViewContactsAsync("deletion", "delete"))
        {
            return;
        }

        var id = AnsiConsole
            .Ask<int>("[CadetBlue]Enter the id of the contact to delete or 0 to return to the previous menu: [/]");

        if (id == 0)
        {
            UiHelpers
                .PressAnyKeyToContinue("Returning to the previous menu");
            return;
        }

        if (! await _phonebookService.DeleteContactAsync(id))
        {
            UiHelpers
                .PressAnyKeyToContinueError("Unable to delete contact at this time\n" +
                "Returning to the previous menu");
            return;
        }

        UiHelpers.PressAnyKeyToContinue("Contact successfully deleted");
    }

    public async Task ViewContactAsync()
    {
        if (!await ViewContactsAsync("choosing a contact to view", "view"))
        {
            return;
        }

        var id = AnsiConsole
            .Ask<int>("[CadetBlue]Enter the id of the contact to view details " +
            "for or 0 to return to the previous menu: [/]");

        if (id == 0)
        {
            UiHelpers
                .PressAnyKeyToContinue("Returning to the previous menu");
            return;
        }

        var contactToView = await _phonebookService.GetContactAsync(id);

        if (contactToView is null)
        {
            UiHelpers
                .PressAnyKeyToContinueError("Unable to view contact details at this time\n" +
                "Returning to the previous menu");
            return;
        }

        Console.Clear();

        ViewHelpers.DisplayContactDetails(contactToView);

        UiHelpers
            .PressAnyKeyToContinue("Finished viewing contact details");
    }

    public async Task ViewAllContactsAsync()
    {
        if (!await ViewContactsAsync("viewing", "view"))
        {
            return;
        }

        UiHelpers.PressAnyKeyToContinue("\nFinshed displaying contacts");
    }

    public async Task SendEmailAsync()
    {
        var wantsToSendEmail = AnsiConsole.Confirm("[DarkSlateGray1]Send an email? [/]"); 

        if (!wantsToSendEmail)
        {
            UiHelpers
                .PressAnyKeyToContinue("Returning to the previous menu");
            return;
        }
        Console.Clear();

        if (! await ViewContactsAsync("choosing a contact to email", "view a contact to email"))
        {
            return;
        }

        var id = AnsiConsole.Ask<int>("[DarkTurquoise]Enter the id of the contact that you wish to send an email to or " +
            "0 to return to the previous menu: [/]");

        if (id == 0)
        {
            UiHelpers
                .PressAnyKeyToContinue("Returning to the previous menu");
            return;
        }

        var contactToEmail = await _phonebookService.GetContactAsync(id);

        if (contactToEmail is null)
        {
            UiHelpers
                .PressAnyKeyToContinueError("Unable to send an email to this contact " +
                "at this time there was an error retrieving contact details\nReturning to the previous menu");
            return;
        }

        var recieverName = $"{contactToEmail.FirstName} {contactToEmail.LastName}";
        var recieverEmail = contactToEmail.Email;
        var senderName = string.Empty;
        var subject = string.Empty;
        var body = string.Empty;

        var isSatisfiedWithEmail = false;

        if (string.IsNullOrEmpty(recieverEmail))
        {
            UiHelpers
                .PressAnyKeyToContinueError("This contact does not have an email address\n" +
                "Unable to send an email\nReturning to the previous menu");
            return;
        }

        while (!isSatisfiedWithEmail)
        {
            Console.Clear();
            senderName = AnsiConsole.Ask<string>("[DarkSeaGreen1]Enter your name: [/]");
            subject = AnsiConsole.Ask<string>("[DarkSeaGreen1]Enter the email subject: [/]");
            body = AnsiConsole.Ask<string>("[DarkSeaGreen1]Enter email body: [/]");

            isSatisfiedWithEmail = AnsiConsole.Confirm("[DarkSlateGray1]Are you satisfied " +
                "with the email that you wrote? [/]");
        }

        var email = new EmailData
        {
            SenderName = senderName,
            RecieverName = recieverName,
            RecieverEmail = recieverEmail,
            Subject = subject,
            Body = body
        };

        Console.Clear();
        wantsToSendEmail = AnsiConsole.Confirm("[DarkSlateGray1]Ready to send email? [/]");

        if (!wantsToSendEmail)
        {
            UiHelpers
                .PressAnyKeyToContinue("Returning to the main menu");
            return;
        }

        if (! await _emailService.SendEmailAsync(email))
        {
            UiHelpers
                .PressAnyKeyToContinueError("There was an error sending your email\n" +
                "Returning to the main menu");
            return;
        }

        UiHelpers.PressAnyKeyToContinue("Email sent successfully!");
    }


    private int DisplayPrompt(PagedList<RetrievedContactDto> contacts, int page)
    {
        var prompt = new SelectionPrompt<Choice>()
                .Title("[CadetBlue_1]Navigate pages[/]");

        if (page > 1)
        {
            prompt.AddChoice(Choice.Previous);
        }
        if (page < contacts.TotalPages)
        {
            prompt.AddChoice(Choice.Next);
        }

        prompt.AddChoice(Choice.Quit);

        var choice = AnsiConsole.Prompt(prompt);

        if (choice == Choice.Next && page < contacts.TotalPages)
        {
            page++;
            Console.Clear();
            return page;
        }
        else if (choice == Choice.Previous && page > 1)
        {
            page--;
            Console.Clear();
            return page;
        }
        else
        {
            return -1;
        }
    }

    private async Task<bool> ViewContactsAsync(string purpose, string errorPurpose)
    {
        var page = 1;
        var count = await _phonebookService.GetContactCountAsync();
        var pageSize = AnsiConsole
            .Ask<int>($"[Aqua]You have a total of {count} contacts to choose from for {purpose}.\n" +
            $"How many would you like to see per page? [/]");
        Console.Clear();

        while (true)
        {
            var contacts = await _phonebookService.GetContactsAsync(page, pageSize);

            if (contacts.Count == 0)
            {
                UiHelpers
                    .PressAnyKeyToContinueError($"No contacts available to {errorPurpose}\n" +
                    $"Returning to the previous menu");
                return false;
            }

            ViewHelpers.DisplayContacts(contacts);

            page = DisplayPrompt(contacts, page);

            if (page == -1) break;
        }

        return true;
    }
}
