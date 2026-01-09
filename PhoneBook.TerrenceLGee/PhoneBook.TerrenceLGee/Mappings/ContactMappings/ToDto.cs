using PhoneBook.TerrenceLGee.DTOs.ContactDTOs;
using PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;
using PhoneBook.TerrenceLGee.Entities;
using PhoneBook.TerrenceLGee.Extensions.Pagination;
using PhoneBook.TerrenceLGee.Mappings.ContactPhoneNumberMappings;

namespace PhoneBook.TerrenceLGee.Mappings.ContactMappings;

public static class ToDto
{
    extension(Contact contact)
    {
        public RetrievedContactDto ToRetrievedContactDto()
        {
            return new RetrievedContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumbers = contact.GetPhoneNumbers(),
                Category = contact.Category,
                Notes = contact.Notes,
                Suffix = contact.Suffix,
                DateOfBirth = contact.DateOfBirth,
                NickName = contact.NickName
            };
        }

        private List<RetrievedContactPhoneNumberDto> GetPhoneNumbers()
        {
            return contact.PhoneNumbers
                .Select(p => p.ToRetrievedContactPhoneNumberDto())
                .ToList();
        }
    }

    extension(PagedList<Contact> contacts)
    {
        public PagedList<RetrievedContactDto> ToRetrievedContactDtoPagedList(int page, int pageSize)
        {
            var retrievedContacts = contacts
                .Select(c => c.ToRetrievedContactDto());

            return new PagedList<RetrievedContactDto>(retrievedContacts, contacts.TotalEntities, page, pageSize);
        }
    }
}
