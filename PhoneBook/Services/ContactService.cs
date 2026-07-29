using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using PhoneBook.Repository;
namespace PhoneBook.Services;

public class ContactService
{
    private readonly PhoneBookContext _phoneBookContext;

    public ContactService()
    {
        _phoneBookContext = new PhoneBookContext();
    }
    internal async Task DeleteContactAsync(int id)
    {
        var contact = await _phoneBookContext.Contacts.FindAsync(id);
        _phoneBookContext.Contacts.Remove(contact!);
        await _phoneBookContext.SaveChangesAsync();
    }

    internal async Task InsertContactAsync(Contact contact)
    {
        _phoneBookContext.Contacts.Add(contact);
        await _phoneBookContext.SaveChangesAsync();
    }

    public async Task ModifyContactAsync(Contact contact)
    {
        contact.UpdatedAt = DateTime.UtcNow;

        bool exists = await _phoneBookContext.Contacts.AnyAsync(c => c.Id == contact.Id);
        if (!exists)
            throw new InvalidOperationException($"No contact found with Id {contact.Id}.");

        _phoneBookContext.Contacts.Update(contact);
        await _phoneBookContext.SaveChangesAsync();
    }

    internal async Task<List<Contact>> SearchByNameAsync(string firstName, string lastName)
    {
        return await _phoneBookContext.Contacts
     .Include(c => c.PhoneNumbers)
     .Include(c => c.Emails)
     .Where(c => c.FirstName.ToLower() == firstName.ToLower()
              && c.LastName.ToLower() == lastName.ToLower())
     .ToListAsync();
    }

    internal async Task<List<Contact>> ViewAllContactAsync()
    {
        return await _phoneBookContext.Contacts
    .Include(c => c.PhoneNumbers)
    .Include(c => c.Emails)
    .ToListAsync();
    }
}
