using Microsoft.EntityFrameworkCore;
using Phonebook.EntityFramework.Domain.Entities;

namespace Phonebook.EntityFramework.Controllers;

internal class ContactController
{

    internal static void AddContact(string name, string email, string phoneNumber, int categoryId)
    {
        using var db = new ContactContext();
        db.Add(new Contact { Name = name, Email = email, PhoneNumber = phoneNumber, CategoryId = categoryId });
        db.SaveChanges();

    }
    internal static Contact GetContactById(int id)
    {
        using var db = new ContactContext();
        var contact = db.Contacts
                        .Include(c => c.Category)
                        .SingleOrDefault(x => x.Id == id);

        return contact;
    }

    internal static void DeleteContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Remove(contact);
        db.SaveChanges();
    }

    internal static void UpdateContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Update(contact);
        db.SaveChanges();
    }

    internal static List<Contact> GetContacts()
    {
        using var db = new ContactContext();
        var contacts = db.Contacts
                         .Include(c => c.Category)
                         .ToList();
        return contacts;
    }
}
