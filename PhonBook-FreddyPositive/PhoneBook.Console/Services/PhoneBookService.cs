using PhoneBook.Controller;
using PhoneBook.Model;
using PhoneBook.View;

namespace PhoneBook.Services;

internal class PhoneBookService
{
    internal static void AddContact()
    {
        PhoneBookDetails phoneBook = ContactManager.GetContactInput();
        PhoneBookController.InsertContact(phoneBook);
    }

    internal static void SearchContact()
    {
        PhoneBookDTO contactInput = ContactManager.GetUserInput("What Combination Would You like to Search?", false, 1);
        List<PhoneBookDTO> contactSearchResults = PhoneBookController.SearchContacts(contactInput);

        ContactManager.DisplayContacts(contactSearchResults, true);
        Console.Clear();

        SearchContact();
    }

    internal static void ViewAllContacts(bool waitForUser)
    {
        List<PhoneBookDTO> phoneBook = PhoneBookController.GetAllContacts();

        ContactManager.DisplayContacts(phoneBook, waitForUser);
    }

    internal static void UpdateContact()
    {
        PhoneBookDTO contactUpdateInput = ContactManager.GetUserInput("What Contact Detail Would You like to Update?", true, 2);

        bool contactExist = PhoneBookController.CheckContactExist(contactUpdateInput.DisplayId);

        if (contactExist)
        {
            PhoneBookController.UpdateContact(contactUpdateInput);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"Contact does not exist ,Please enter a valid contact id to update");
            Console.WriteLine();
            Thread.Sleep(3000);
        }

        Console.Clear();

        UpdateContact();
    }

    internal static void DeleteContact()
    {
        int ContactId = ContactManager.GetContactIdToDelete();

        bool contactExist = PhoneBookController.CheckContactExist(ContactId);

        if(contactExist)
        {
            PhoneBookController.DeleteContact(ContactId);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"Contact does not exist ,Please enter a valid contact id to delete");
            Console.WriteLine();
            Thread.Sleep(3000);
        }

        Console.Clear();

        DeleteContact();
    }
}
