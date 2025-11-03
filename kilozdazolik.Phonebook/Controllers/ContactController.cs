using kilozdazolik.Phonebook.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace kilozdazolik.Phonebook.Controllers;

public class ContactController
{
    internal static List<Contact> GetContacts()
    {
        try
        {
            using var db = new PhoneBookContext();
            var contacts = db.Contacts.ToList();
            return contacts;
        }
        catch (SqlException ex)
        {
            throw new Exception("Failed to retrieve contacts", ex);
        }
    }

    internal static void InsertContact(Contact contact)
    {
        try
        {
            using var db = new PhoneBookContext(); 
            db.Add(contact);
            db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Database error: ", ex);
        }
        catch (SqlException ex)
        {
            throw new Exception("Unexpected error happened: ", ex);
        }
    }

    internal static void RemoveContact(Contact contact)
    {
        try
        {
            using var db = new PhoneBookContext(); 
            db.Remove(contact);
            db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Database error: ", ex);
        }
        catch (SqlException ex)
        {
            throw new Exception("Unexpected error happened: ", ex);
        }
    }

    internal static void UpdateContact(Contact contact)
    {
        try
        {
            using var db = new PhoneBookContext();
            db.Update(contact);
            db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Database error: ", ex);
        }
        catch (SqlException ex)
        {
            throw new Exception("Unexpected error happened: ", ex);
        }
    }
}