using Microsoft.EntityFrameworkCore;
using Phonebook.Model;

namespace Phonebook;

internal class DatabaseManager
{
    internal List<Contact> ViewContacts()
    {

        try
        {
            using (var context = new PhonebookContext())
            {
                var contacts = context.Contacts.ToList();

                return contacts;
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"\n[Error] Could not load contacts: {ex.Message}");
            return new List<Contact>();
        }
    }

    internal bool AddContact(string name, string phoneNumber, string email)
    {
        try
        {
            using (var context = new PhonebookContext())
            {
                var contact = new Contact()
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    EmailAddress = email
                };

                context.Add(contact);
                context.SaveChanges();
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Unexpected Error] {ex.Message}");
            return false;
        }
    }

    internal List<Contact> FindContact(string name)
    {
        try
        {
            using (var context = new PhonebookContext())
            {
                var contact = context.Contacts.Where(c => c.Name.Contains(name)).OrderBy(n => n.Name);
                return contact.ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Error] Search failed: {ex.Message}");
            return new List<Contact>();
        }
    }

    internal List<Contact> FindContactByIdAndName(int id, string name)
    {
        try
        {
            using (var context = new PhonebookContext())
            {
                var contact = context.Contacts.Where(c => c.Id == id && c.Name.Contains(name));
                return contact.ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Error] Find failed: {ex.Message}");
            return new List<Contact>();
        }
    }

    internal bool DeleteById(int id)
    {
        try
        {
            using (var context = new PhonebookContext())
            {
                var rowsDeleted = context.Contacts.Where(c => c.Id == id).ExecuteDelete();
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Error] Could not delete contact: {ex.Message}");
            return false;
        }
    
    }

    internal bool UpdateById(int id, string name, string phoneNumber, string email)
    {
        try
        {
            using (var context = new PhonebookContext())
            {
                context.Contacts.Where(c => c.Id == id).ExecuteUpdate(setter =>
                {
                    setter.SetProperty(c => c.Name, name);
                    setter.SetProperty(c => c.PhoneNumber, phoneNumber);
                    setter.SetProperty(c => c.EmailAddress, email);

                });

                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Error] Could not update contact: {ex.Message}");
            return false;
        }

    }
}
