using PhoneBook.TerrenceLGee.DTOs.ContactDTOs;
using PhoneBook.TerrenceLGee.Entities;
using PhoneBook.TerrenceLGee.Mappings.ContactPhoneNumberMappings;

namespace PhoneBook.TerrenceLGee.Mappings.ContactMappings;

public static class FromDto
{
    extension(CreateContactDto dto)
    {
        public Contact FromCreateContactDto()
        {
            return new Contact
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumbers = dto.GetPhoneNumbers(),
                Category = dto.Category,
                Notes = dto.Notes,
                Suffix = dto.Suffix,
                DateOfBirth = dto.DateOfBirth,
                NickName = dto.NickName
            };
        }

        private List<ContactPhoneNumber> GetPhoneNumbers()
        {
            return dto.PhoneNumbers
                .Select(p => p.FromCreateContactPhoneNumberDto())
                .ToList();
        }
    }

    extension(UpdateContactDto dto)
    {
        public Contact FromUpdateContactDto()
        {
            return new Contact
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumbers = dto.GetPhoneNumbers(),
                Category = dto.Category,
                Notes = dto.Notes,
                Suffix = dto.Suffix,
                DateOfBirth = dto.DateOfBirth,
                NickName = dto.NickName
            };
        }

        private List<ContactPhoneNumber> GetPhoneNumbers()
        {
            return dto.PhoneNumbers
                .Select(p => p.FromUpdateContactPhoneNumberDto())
                .ToList();
        }
    }
}
