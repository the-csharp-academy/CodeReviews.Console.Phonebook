using Phonebook.Models;
using Spectre.Console;

namespace Phonebook.Database
{
    internal class ContactDbHelper
    {
        internal static List<Contact> GetAllContacts()
        {
            using (ContactContext db = new ContactContext())
            {
                AnsiConsole.WriteLine("Retrieving contacts...");
                List<Contact> allContacts = db.Contacts
                    .OrderBy(c => c.Name)
                    .ToList();
                AnsiConsole.Clear();
                return allContacts;
            }
        }

        internal static void AddContact(Contact contact)
        {
            using (ContactContext db = new ContactContext())
            {
                db.Contacts.Add(contact);
                Console.WriteLine("Saving the contact...");
                db.SaveChanges();
                Console.WriteLine("Save changes complete\n");
            }
        }

        internal static void UpdateContact(Contact contact)
        {
            using (ContactContext db = new ContactContext())
            {
                db.Contacts.Update(contact);
                Console.WriteLine("Updating the contact...");
                db.SaveChanges();
                Console.WriteLine("Update complete\n");
            }
        }

        internal static void DeleteContact(Contact contactToDelete)
        {
            using (ContactContext db = new ContactContext())
            {
                db.Contacts.Remove(contactToDelete);
                Console.WriteLine("Deleting the contact...");
                db.SaveChanges();
                Console.WriteLine("Delete complete\n");
            }
        }
    }
}
