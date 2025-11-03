using kilozdazolik.Phonebook.Models;
using Microsoft.EntityFrameworkCore;

namespace kilozdazolik.Phonebook.Controllers;

public class ContactController
{
    internal static List<Contact> GetContacts()
    {
        using var db = new PhoneBookContext();

        var contacts = db.Contacts.ToList();
        return contacts;
    }
}