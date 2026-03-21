using phoneBook._0lcm.Enums;
using phoneBook._0lcm.Models;
using phoneBook._0lcm.ServiceContracts;
using Spectre.Console;
using Spectre.Console.Rendering;
using static phoneBook._0lcm.UserInterface.DisplayHelper;

namespace phoneBook._0lcm.UserInterface;

public class ContactUi(
    IVerificationService verificationService,
    IContactService contactService,
    MessagingUi messagingUi)
{
    private const string ReturnOption = "* Back";

    //------- Crud Methods -------
    internal void AddNewContact()
    {
        Console.Clear();
        var contact = GetContactClass();

        contactService.SaveContactInfo(contact);

        DisplaySuccess("Successfully added contact to contact list.");
        WaitForUser();
    }

    internal void ViewContacts()
    {
        while (true)
        {
            var classMap = BuildContactClassMap();
            Console.Clear();

            var mapKey = DisplayPrompt(classMap.Keys.ToList(), "Select a contact, or press Back to return:");
            if (classMap[mapKey] == null) return;

            LoadContactDetails(classMap[mapKey]!);
        }
    }

    private void LoadContactDetails(ContactClass contactClass)
    {
        Console.Clear();
        var properties = new List<IRenderable>
        {
            new Markup($"[{White}]Name: [/] [{Grey}]{contactClass.Name}[/]"),
            new Markup($"[{White}]Phone Number: [/][{Grey}]{contactClass.PhoneNumber}[/]"),
            new Markup($"[{White}]Email: [/][{Grey}]{contactClass.Email}[/]")
        };

        DisplayRows(properties);
        DisplayMessage("\n");
        var option = DisplayMenu<ContactViewingOption>("\nSelect an option to continue:");

        switch (option)
        {
            case ContactViewingOption.EditContact:
                EditContact(contactClass);
                break;
            case ContactViewingOption.DeleteContact:
                DeleteContact(contactClass);
                break;
            case ContactViewingOption.SendAnEmail:
                messagingUi.WriteAndSendEmail(contactClass);
                break;
            case ContactViewingOption.SendAnSmsMessage:
                messagingUi.WriteAndSendSms(contactClass);
                break;
            case ContactViewingOption.Back:
                return;
        }
    }

    private void EditContact(ContactClass oldContactClass)
    {
        Console.Clear();
        var newContact = GetContactClass();
        contactService.EditContactClass(newContact, oldContactClass);

        DisplaySuccess("Successfully edited contact info.");
        WaitForUser();
    }

    private void DeleteContact(ContactClass contactClass)
    {
        Console.Clear();
        if (AnsiConsole.Confirm($"[{Red}]are you sure you want to delete this contact?[/]"))
        {
            contactService.DeleteContactClass(contactClass);
            DisplaySuccess("Successfully deleted contact info.");
            WaitForUser();
        }
        else
        {
            DisplaySuccess("The deletion was cancelled.");
            WaitForUser();
        }
    }

    //------- Helper Methods -------
    private ContactClass GetContactClass()
    {
        var contactName = DisplayQuestion("Please enter a name:");
        string contactEmail;
        string contactPhone;

        while (true)
        {
            Console.Clear();
            contactEmail = DisplayQuestion("Please enter a valid email: (Eg. JohnDoe@gmail.com)");
            if (!verificationService.ValidateEmail(contactEmail))
            {
                DisplayWarning(
                    "The email you entered is not valid. Please make sure you have a valid body, '@', and domain name.");
                WaitForUser();
                continue;
            }

            break;
        }

        while (true)
        {
            Console.Clear();
            DisplayInfo("Please include a country code such as: +1 (000)-000-0000, or +20 11 11111111.");
            contactPhone = DisplayQuestion("Please enter a phone number:");
            if (!verificationService.ValidatePhoneNumber(contactPhone, "ZZ"))
            {
                DisplayWarning(
                    "Please enter a valid phone number. Make sure the phone number matches with your country code.");
                WaitForUser();
                continue;
            }

            break;
        }

        return new ContactClass
        {
            Name = contactName,
            PhoneNumber = contactPhone,
            Email = contactEmail
        };
    }

    private void WaitForUser()
    {
        DisplayInfo("Press enter to continue.");
        Console.ReadLine();
    }

    /// <summary>
    ///     Builds a dictionary map between a display string and a ContactClass instance
    /// </summary>
    /// <returns>A built map, with null as the value for the ReturnOption key</returns>
    private Dictionary<string, ContactClass?> BuildContactClassMap()
    {
        var contactClasses = contactService.GetContactClasses();
        var classMap = new Dictionary<string, ContactClass?>();

        classMap[ReturnOption] = null;

        foreach (var contact in contactClasses)
        {
            var display = $" - {contact.Name}";
            classMap[display] = contact;
        }

        return classMap;
    }
}