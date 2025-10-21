using PhoneBook.EntityDTOMapping;
using PhoneBook.Model;

namespace PhoneBook.Controller;

internal class PhoneBookController
{
    internal static void InsertContact(PhoneBookDetails phoneBook)
    {
        try
        {
            using var db = new PhoneBookDbContext();

            db.Add(phoneBook);
            db.SaveChanges();

            Console.WriteLine();
            Console.WriteLine($"Contact has been created succesfully");
            Console.WriteLine();
            Thread.Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }

    internal static List<PhoneBookDTO> GetAllContacts()
    {
        using var db = new PhoneBookDbContext();

        var contacts = db.PhoneBookDetails.ToList();

        var contactlist = ContactMapper.PhoneBookList(contacts);

        return contactlist;
    }

    internal static bool IsEmailAddressAlreadyExist(string emailAddress)
    {
        using var db = new PhoneBookDbContext();
        return db.PhoneBookDetails.Any(contact => contact.EmailAddress == emailAddress);
    }

    internal static bool IsPhoneNumberAlreadyExist(string phoneNumber)
    {
        using var db = new PhoneBookDbContext();
        return db.PhoneBookDetails.Any(contact => contact.PhoneNumber == phoneNumber);
    }

    internal static List<PhoneBookDTO> SearchContacts(PhoneBookDTO contact)
    {
        using var db = new PhoneBookDbContext();

        var query = db.PhoneBookDetails.AsQueryable();

        if (!string.IsNullOrEmpty(contact.EmailAddress))
        {
            query = query.Where(c => c.EmailAddress == contact.EmailAddress);
        }
        if (!string.IsNullOrEmpty(contact.Name))
        {
            query = query.Where(c => c.Name == contact.Name);
        }
        if (!string.IsNullOrEmpty(contact.PhoneNumber))
        {
            query = query.Where(c => c.PhoneNumber == contact.PhoneNumber);
        }

        var searchResult = query.ToList();
        var searchResultDTO = ContactMapper.PhoneBookList(searchResult);

        return searchResultDTO;
    }

    internal static void UpdateContact(PhoneBookDTO contactUpdateInput)
    {
        try
        {
            using var db = new PhoneBookDbContext();

            var contactsList = db.PhoneBookDetails
                        .OrderBy(c => c.Id)
                        .ToList();

            int contactId = contactUpdateInput.DisplayId - 1;

            var contactToUpdate = contactsList[contactId];

            if (!string.IsNullOrEmpty(contactUpdateInput.Name))
                contactToUpdate.Name = contactUpdateInput.Name;

            if (!string.IsNullOrEmpty(contactUpdateInput.EmailAddress))
                contactToUpdate.EmailAddress = contactUpdateInput.EmailAddress;

            if (!string.IsNullOrEmpty(contactUpdateInput.PhoneNumber))
                contactToUpdate.PhoneNumber = contactUpdateInput.PhoneNumber;

            db.SaveChanges();

            Console.WriteLine();
            Console.WriteLine($"Contact has been updated succesfully");
            Console.WriteLine();
            Thread.Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }

    internal static void DeleteContact(int contactId)
    {
        try
        {
            using var db = new PhoneBookDbContext();

            var allContacts = db.PhoneBookDetails
                        .OrderBy(c => c.Id)
                        .ToList();

            int displayIndex = contactId - 1;

            var entityToUpdate = allContacts[displayIndex];

            db.Remove(entityToUpdate);
            db.SaveChanges();

            Console.WriteLine();
            Console.WriteLine($"Contact has been deleted succesfully");
            Console.WriteLine();
            Thread.Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }

    internal static bool CheckContactExist(int contactId)
    {
        bool contactExist = true;

        using var db = new PhoneBookDbContext();

        var allContacts = db.PhoneBookDetails
                    .OrderBy(c => c.Id)
                    .ToList();

        int displayIndex = contactId - 1;

        if (displayIndex < 0 || displayIndex >= allContacts.Count)
        {
            contactExist = false;
        }

        return contactExist;
    }
}
