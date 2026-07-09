using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Controller;

internal class ContactController
{
    internal static bool AddContact(Contact contact) 
    {
        try
        {
            using var db = new PhoneBookContext();
            db.Add(contact);
            return db.SaveChanges() > 0;
        }
        catch(Exception ex)  
        {
            AnsiConsole.MarkupLine($"[red]Error adding contact:[/] {ex.Message}");
            return false;
        }
    }

    internal static List<Contact> GetAllContacts() 
    {
        try
        {
            using var db = new PhoneBookContext();

            List<Contact> contacts = db.Contacts
                .Include(c => c.Category)
                .ToList();

            return contacts;
        }
        catch (Exception ex) 
        {
            AnsiConsole.MarkupLine($"[red]Error loading contacts:[/] {ex.Message}");
            return [];
        }
    }

    internal static Contact GetContactById(int id) 
    {
        try
        {
            using var db = new PhoneBookContext();
            var contact = db.Contacts
                .Include(c => c.Category)
                .SingleOrDefault(x => x.ContactId == id);

            return contact;
        }
        catch (Exception ex) 
        {
            AnsiConsole.MarkupLine($"[red]Error loading contact:[/] {ex.Message}");
            return null;
        }
    }

    internal static bool UpdateContact(Contact contact) 
    {
        try
        {
            using var db = new PhoneBookContext();
            db.Update(contact);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex) 
        {
            AnsiConsole.MarkupLine($"[red]Error updating contact:[/] {ex.Message}");
            return false;
        }
    }

    internal static bool DeleteContact(Contact contact) 
    {
        try
        {
            using var db = new PhoneBookContext();
            db.Remove(contact);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex) 
        {
            AnsiConsole.MarkupLine($"[red]Error deleting contact:[/] {ex.Message}");
            return false;
        }
    }

    internal static bool NameExist(string name)
    {
        using var db = new PhoneBookContext();

        return db.Contacts.Any(c => c.Name == name);
    }

    internal static bool EmailExist(string email)
    {
        using var db = new PhoneBookContext();

        return db.Contacts.Any(c => c.Email == email);
    }
}