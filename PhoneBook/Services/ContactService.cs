using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Services;

public class ContactService
{
    private readonly Func<PhoneBookContext> _contextFactory;

    public ContactService(Func<PhoneBookContext>? contextFactory = null)
    {
        _contextFactory = contextFactory ?? (() => new PhoneBookContext());
    }

    public List<Contact> GetAllContacts()
    {
        using var context = _contextFactory();
        return context.Contacts
            .Include(c => c.Category)
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToList();
    }

    public Contact? GetContactById(int id)
    {
        using var context = _contextFactory();
        return context.Contacts
            .Include(c => c.Category)
            .AsNoTracking()
            .FirstOrDefault(c => c.Id == id);
    }

    public void AddContact(Contact contact)
    {
        ArgumentNullException.ThrowIfNull(contact);

        using var context = _contextFactory();
        context.Contacts.Add(contact);
        context.SaveChanges();
    }

    public bool UpdateContact(Contact contact)
    {
        ArgumentNullException.ThrowIfNull(contact);

        using var context = _contextFactory();
        var existingContact = context.Contacts.Find(contact.Id);
        if (existingContact is null)
        {
            return false;
        }

        existingContact.Name = contact.Name;
        existingContact.PhoneNumber = contact.PhoneNumber;
        existingContact.EmailAddress = contact.EmailAddress;
        existingContact.CategoryId = contact.CategoryId;
        context.SaveChanges();
        return true;
    }

    public bool DeleteContact(int id)
    {
        using var context = _contextFactory();
        var contact = context.Contacts.Find(id);

        if (contact is null)
        {
            return false;
        }

        context.Contacts.Remove(contact);
        context.SaveChanges();
        return true;
    }

    public List<Contact> SearchContact(string searchTerms)
    {
        if (string.IsNullOrWhiteSpace(searchTerms))
        {
            throw new ArgumentException("A search term is required.", nameof(searchTerms));
        }

        using var context = _contextFactory();
        return context.Contacts
            .Include(c => c.Category)
            .AsNoTracking()
            .Where(c => EF.Functions.Like(c.Name, $"%{searchTerms}%"))
            .OrderBy(c => c.Name)
            .ToList();
    }
}
