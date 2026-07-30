


using Spectre.Console;

namespace PhoneBook.EntityFramework
{
    internal class ContactController
    {
        internal static void AddContact(string name, string email, string phone)
        {

            using var db = new ContactsContext();
            db.Add(new Contact { Name = name, Email = email, Phone = phone });

            db.SaveChanges();

        }

        internal static void DeleteContact(Contact contact)
        {
            using var db = new ContactsContext();
            db.Remove(contact);
            db.SaveChanges();

        }

        internal static void UpdateContact(Contact contact)
        {
            using var db = new ContactsContext();
            db.Update(contact);

            db.SaveChanges();


        }

        internal static List<Contact> ViewAllContacts()
        {
            using var db = new ContactsContext();

            var contacts = db.Contacts.ToList();
            //access db, create variable that returns what the method is asking for 
            //in this case all the contacts in the db in a list
            return contacts;
        }
        internal static Contact ViewContactById(int id)
        {
            using var db = new ContactsContext();
            var contact = db.Contacts.SingleOrDefault(x => x.Id == id);
            //using LINQ to check if contact id matches the id passed into the method.

            //access db, create variable that returns what the method is asking for 
            //in this caase a single contact 
            return contact; 
        }
        internal static void ViewContact()
        {
            throw new NotImplementedException();
        }
    }
}
