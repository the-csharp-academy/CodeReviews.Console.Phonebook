using phoneBook._0lcm.Data;
using phoneBook._0lcm.Models;
using phoneBook._0lcm.ServiceContracts;

namespace phoneBook._0lcm.Services;

public class ContactService(AppDbContext db) : IContactService
{
    public async Task SaveContactInfo(ContactClass contact)
    {
        db.ContactClasses.Add(contact);
        await db.SaveChangesAsync();
    }

    public List<ContactClass> GetContactClasses()
    {
        return db.ContactClasses.ToList();
    }

    public async Task EditContactClass(ContactClass newContactClass, ContactClass oldContactClass)
    {
        var oldContact = await db.ContactClasses.FindAsync(oldContactClass.Id);

        if (oldContact is null)
        {
            db.ContactClasses.Add(newContactClass);
        }
        else
        {
            oldContact.Name = newContactClass.Name;
            oldContact.Email = newContactClass.Email;
            oldContact.PhoneNumber = newContactClass.PhoneNumber;
        }

        await db.SaveChangesAsync();
    }

    public async Task DeleteContactClass(ContactClass contactClass)
    {
        db.ContactClasses.Remove(contactClass);
        await db.SaveChangesAsync();
    }
}