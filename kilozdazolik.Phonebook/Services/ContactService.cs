using kilozdazolik.Phonebook.Controllers;

namespace kilozdazolik.Phonebook.Services;

public class ContactService
{
    internal static void GetContacts()
    {
        var contacts = ContactController.GetContacts();
        UserInterface.ShowContactsTable(contacts);
    }
}