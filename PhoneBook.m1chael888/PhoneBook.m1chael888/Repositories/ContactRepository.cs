using PhoneBook.m1chael888.Models;
using PhoneBook.m1chael888.Data;

namespace PhoneBook.m1chael888.Repositories
{
    public interface IContactRepository
    {
        void Create(Contact contact);
        List<Contact> Read();
        void Update(Contact updatedContact);
        void Delete(int id);
    }
    internal class ContactRepository : IContactRepository
    {
        public void Create(Contact contact)
        {
            using PhoneBookContext context = new PhoneBookContext();

            context.Add(contact);
            context.SaveChanges();
        }

        public List<Contact> Read()
        {
            using PhoneBookContext context = new PhoneBookContext();

            var contacts = context.Contacts.ToList();
            return contacts;
        }

        public void Update(Contact updatedContact)
        {
            using PhoneBookContext context = new PhoneBookContext();

            var existingContact = context.Contacts
                .Where(x => x.Id == updatedContact.Id)
                .FirstOrDefault();

            if (existingContact is Contact)
            {
                existingContact.Name = updatedContact.Name;
                existingContact.Email = updatedContact.Email;
                existingContact.PhoneNumber = updatedContact.PhoneNumber;
            }
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            using PhoneBookContext context = new PhoneBookContext();

            var contact = context.Contacts
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (contact is Contact) context.Contacts.Remove(contact);
            context.SaveChanges();
        }
    }
}
