using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook
{
    internal class ContactController
    {
        public void CreateContact(Contact userContact)
        {
            using var db = new ContactContext();
            db.Contacts.Add(userContact);

            SaveChanges(db);
        }

        public List<Contact> GetAllContacts()
        {
            using var db = new ContactContext();
            List<Contact> contacts = db.Contacts.Include(contact => contact.Category).ToList();
            return contacts;
        }

        public void UpdateContact(Contact userContact)
        {
            using var db = new ContactContext();
            db.Contacts.Update(userContact);

            SaveChanges(db);
        }

        public void DeleteContact(Contact userContact)
        {
            using var db = new ContactContext();
            db.Contacts.Remove(userContact);

            SaveChanges(db);
        }

        public void SaveChanges(ContactContext db)
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                AnsiConsole.MarkupLine("[Red]ERROR [/]Couldn't save changes");
                Environment.Exit(0);
            }
            catch (DbUpdateException)
            {
                AnsiConsole.MarkupLine("[Red]ERROR [/]Couldn't save changes");
                Environment.Exit(0);
            }
        }
    }
}