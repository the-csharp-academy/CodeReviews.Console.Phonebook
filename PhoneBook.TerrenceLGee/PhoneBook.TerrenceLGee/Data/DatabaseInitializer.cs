using PhoneBook.TerrenceLGee.Entities;
using PhoneBook.TerrenceLGee.Entities.Enums;

namespace PhoneBook.TerrenceLGee.Data;

public static class DatabaseInitializer
{
    public static void Initialize(PhonebookContext context)
    {
        if (context.Contacts.Any()) return;

        var phoneNumber1 = new ContactPhoneNumber
        {
            Number = "555-123-4567",
            CountryCode = CountryCode.US,
            PhoneNumberCategory = PhoneNumberCategory.Home,
        };

        var phoneNumber2 = new ContactPhoneNumber
        {
            Number = "555-765-4321",
            CountryCode = CountryCode.US,
            PhoneNumberCategory = PhoneNumberCategory.Work,
        };


        var phoneNumber3 = new ContactPhoneNumber
        {
            Number = "555-321-6321",
            CountryCode = CountryCode.US,
            PhoneNumberCategory = PhoneNumberCategory.Work,
        };

        var phoneNumber4 = new ContactPhoneNumber
        {
            Number = "555-123-9966",
            CountryCode = CountryCode.US,
            PhoneNumberCategory = PhoneNumberCategory.Mobile,
        };

        var contact1 = new Contact
        {
            FirstName = "John",
            LastName = "Smith",
            Email = "jsmith@gmail.con",
            Category = ContactCategory.Friends,
            Notes = "Friend since high school",
            DateOfBirth = DateOnly.Parse("09-12-1979"),
            PhoneNumbers = new List<ContactPhoneNumber> { phoneNumber1, phoneNumber2}
        };

        var contact2 = new Contact
        {
            FirstName = "Reginald",
            LastName = "Mixson",
            Suffix = "Jr",
            Email = "rmixson@yahoo.com",
            Category = ContactCategory.Family,
            Notes = "First cousin",
            NickName = "Reggie",
            DateOfBirth = DateOnly.Parse("08-16-1981"),
            PhoneNumbers = new List<ContactPhoneNumber> { phoneNumber3 }
        };

        var contact3 = new Contact
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@outlook.com",
            Category = ContactCategory.Friends,
            Notes = "John's wife",
            DateOfBirth = DateOnly.Parse("01-13-82"),
            PhoneNumbers = new List<ContactPhoneNumber> { phoneNumber4 }
        };

        var contacts = new Contact[]
        {
            contact1,
            contact2,
            contact3
        };

        context.AddRange(contacts);
        context.SaveChanges();
    }
}
