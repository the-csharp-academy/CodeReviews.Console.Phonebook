using PhoneBook.TerrenceLGee.DTOs.ContactDTOs;
using PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;
using PhoneBook.TerrenceLGee.Entities;
using PhoneBook.TerrenceLGee.Entities.Enums;
using PhoneBook.TerrenceLGee.Extensions.Pagination;
using PhoneBook.TerrenceLGee.SendEmailModels;

namespace Phonebook.TerrenceLGee.Tests;

public static class Resources
{
    public static CreateContactDto GetCreateContactDto()
    {
        return new()
        {
            FirstName = "Susan",
            LastName = "Johnson",
            Email = "susan.johnson@hotmail.com",
            PhoneNumbers = new List<CreateContactPhoneNumberDto>
                {
                    new()
                    {
                        Number = "555-555-2424",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home
                    },
                    new()
                    {
                        Number = "555-123-3366",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Mobile
                    },
                    new()
                    {
                        Number = "555-369-1269",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Work,
                    }
                },
            Category = ContactCategory.Friends,
            Notes = "High school class mate",
            Suffix = null,
            DateOfBirth = new DateOnly(1978, 10, 01),
            NickName = "Susie",
        };
    }

    public static UpdateContactDto GetUpdateContactDto()
    {
        return new()
        {
            Id = 2,
            FirstName = "Susan",
            LastName = "Johnson",
            Email = "susan.johnson@hotmail.com",
            PhoneNumbers = new List<UpdateContactPhoneNumberDto>
                {
                    new()
                    {
                        Id = 2,
                        Number = "555-555-2424",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 3,
                        Number = "555-123-3366",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Mobile,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 4,
                        Number = "555-369-1269",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Work,
                        ContactId = 2,
                    }
                }
        };
    }

    public static RetrievedContactDto GetRetrievedContactDto()
    {
        return new()
        {
            Id = 2,
            FirstName = "Susan",
            LastName = "Johnson",
            Email = "susan.johnson@hotmail.com",
            PhoneNumbers = new List<RetrievedContactPhoneNumberDto>
                {
                    new()
                    {
                        Id = 2,
                        Number = "555-555-2424",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 3,
                        Number = "555-123-3366",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Mobile,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 4,
                        Number = "555-369-1269",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Work,
                        ContactId = 2,
                    }
                },
            Category = ContactCategory.Friends,
            Notes = "High school class mate from 10th through 12th grades",
            Suffix = null,
            DateOfBirth = new DateOnly(1978, 10, 01),
            NickName = "Susie",
        };
    }

    public static Contact GetContact()
    {
        return new()
        {
            Id = 2,
            FirstName = "Susan",
            LastName = "Johnson",
            Email = "susan.johnson@hotmail.com",
            PhoneNumbers = new List<ContactPhoneNumber>
                {
                    new()
                    {
                        Id = 2,
                        Number = "555-555-2424",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 3,
                        Number = "555-123-3366",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Mobile,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 4,
                        Number = "555-369-1269",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Work,
                        ContactId = 2,
                    }
                },
            Category = ContactCategory.Friends,
            Notes = "High school class mate from 10th through 12th grades",
            Suffix = null,
            DateOfBirth = new DateOnly(1978, 10, 01),
            NickName = "Susie",
        };
    }

    public static List<Contact> GetContacts()
    {
        return new List<Contact>
        {
             new()
            {
                Id = 1,
                FirstName = "Alan",
                LastName = "Smith",
                Email = "alan_smith@proton.me",
                PhoneNumbers = new List<ContactPhoneNumber>
                {
                    new()
                    {
                        Id = 1,
                        Number = "555-555-1212",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home,
                        ContactId = 1,
                    }
                },
                Category = ContactCategory.Work,
                Notes = "Brand new co-worker",
                Suffix = null,
                DateOfBirth = new DateOnly(1995, 07, 03),
                NickName = null
            },
            new()
            {
                Id = 2,
                FirstName = "Susan",
                LastName = "Johnson",
                Email = "susan.johnson@hotmail.com",
                PhoneNumbers = new List<ContactPhoneNumber>
                {
                    new()
                    {
                        Id = 2,
                        Number = "555-555-2424",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 3,
                        Number = "555-123-3366",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Mobile,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 4,
                        Number = "555-369-1269",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Work,
                        ContactId = 2,
                    }
                },
                Category = ContactCategory.Friends,
                Notes = "High school class mate from 10th through 12th grades",
                Suffix = null,
                DateOfBirth = new DateOnly(1978, 10, 01),
                NickName = "Susie",
            },
            new()
            {
                Id = 3,
                FirstName = "Jacob",
                LastName = "Thomas",
                Email = "j_thomas@gmail.com",
                PhoneNumbers = new List<ContactPhoneNumber>
                {
                    new()
                    {
                        Id = 5,
                        Number = "555-549-3636",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home,
                        ContactId = 3,
                    }
                },
                Category = ContactCategory.Business,
                Notes = "New client who has commissed me to build an API for his business",
                Suffix = "Jr.",
                DateOfBirth = new DateOnly(1985, 02, 23),
                NickName = null
            }
        };
    }

    public static List<RetrievedContactDto> GetRetrievedContactDtos()
    {
        return new List<RetrievedContactDto>
        {
             new()
            {
                Id = 1,
                FirstName = "Alan",
                LastName = "Smith",
                Email = "alan_smith@proton.me",
                PhoneNumbers = new List<RetrievedContactPhoneNumberDto>
                {
                    new()
                    {
                        Id = 1,
                        Number = "555-555-1212",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home,
                        ContactId = 1,
                    }
                },
                Category = ContactCategory.Work,
                Notes = "Brand new co-worker",
                Suffix = null,
                DateOfBirth = new DateOnly(1995, 07, 03),
                NickName = null
            },
            new()
            {
                Id = 2,
                FirstName = "Susan",
                LastName = "Johnson",
                Email = "susan.johnson@hotmail.com",
                PhoneNumbers = new List<RetrievedContactPhoneNumberDto>
                {
                    new()
                    {
                        Id = 2,
                        Number = "555-555-2424",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 3,
                        Number = "555-123-3366",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Mobile,
                        ContactId = 2,
                    },
                    new()
                    {
                        Id = 4,
                        Number = "555-369-1269",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Work,
                        ContactId = 2,
                    }
                },
                Category = ContactCategory.Friends,
                Notes = "High school class mate from 10th through 12th grades",
                Suffix = null,
                DateOfBirth = new DateOnly(1978, 10, 01),
                NickName = "Susie",
            },
            new()
            {
                Id = 3,
                FirstName = "Jacob",
                LastName = "Thomas",
                Email = "j_thomas@gmail.com",
                PhoneNumbers = new List<RetrievedContactPhoneNumberDto>
                {
                    new()
                    {
                        Id = 5,
                        Number = "555-549-3636",
                        CountryCode = CountryCode.US,
                        PhoneNumberCategory = PhoneNumberCategory.Home,
                        ContactId = 3,
                    }
                },
                Category = ContactCategory.Business,
                Notes = "New client who has commissed me to build an API for his business",
                Suffix = "Jr.",
                DateOfBirth = new DateOnly(1985, 02, 23),
                NickName = null
            }
        };
    }

    public static PagedList<Contact> GetPagedContacts()
    {
        var items = GetContacts();
        var count = items.Count;
        var page = 1;
        var pageSize = 3;

        return new PagedList<Contact>(items, count, page, pageSize);
    }

    public static PagedList<RetrievedContactDto> GetPagedRetrievedContactDtos()
    {
        var items = GetRetrievedContactDtos();
        var count = items.Count;
        var page = 1;
        var pageSize = 3;

        return new PagedList<RetrievedContactDto>(items, count, page, pageSize);
    }

    public static EmailData GetEmailData()
    {
        return new()
        {
            SenderName = "Jacob Thomas",
            RecieverName = "Susan Johnson",
            RecieverEmail = "susan.johnson@hotmail.com",
            Subject = "Lunch meeting",
            Body = "Are we still on for lunch this coming Friday at 12:30 pm?",
        };
    }

    public static Dictionary<string, string?> GetConfiguration()
    {
        return new Dictionary<string, string?>
        {
            {"EMAIL_CONFIGURATION:HOST","smtp.test.com" },
            {"EMAIL_CONFIGURATION:PORT", "123"},
            {"EMAIL_CONFIGURATION:EMAIL", "j_thomas@gmail.com"},
            {"EMAIL_CONFIGURATION:PASSWORD", "Te$tP@$$w0rd"},
        };
    }
}
