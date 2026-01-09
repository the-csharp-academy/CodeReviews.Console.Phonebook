using PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;
using PhoneBook.TerrenceLGee.Entities;

namespace PhoneBook.TerrenceLGee.Mappings.ContactPhoneNumberMappings;

public static class FromDto
{
    extension(CreateContactPhoneNumberDto dto)
    {
        public ContactPhoneNumber FromCreateContactPhoneNumberDto()
        {
            return new ContactPhoneNumber
            {
                Number = dto.Number,
                CountryCode = dto.CountryCode,
                PhoneNumberCategory = dto.PhoneNumberCategory
            };
        }

        public UpdateContactPhoneNumberDto FromCreateToUpdateContactPhoneNumberDto()
        {
            return new UpdateContactPhoneNumberDto
            {
                Number = dto.Number,
                CountryCode = dto.CountryCode,
                PhoneNumberCategory = dto.PhoneNumberCategory
            };
        }
    }

    extension(List<CreateContactPhoneNumberDto> dtos)
    {
        public List<UpdateContactPhoneNumberDto> FromCreateToUpdateContactPhoneNumberDtoList()
        {
            return dtos
                .Select(p => p.FromCreateToUpdateContactPhoneNumberDto())
                .ToList();
        }
    }

    extension(UpdateContactPhoneNumberDto dto)
    {
        public ContactPhoneNumber FromUpdateContactPhoneNumberDto()
        {
            return new ContactPhoneNumber
            {
                Id = dto.Id,
                Number = dto.Number,
                CountryCode = dto.CountryCode,
                PhoneNumberCategory = dto.PhoneNumberCategory,
                ContactId = dto.ContactId
            };
        }
    }

    extension(RetrievedContactPhoneNumberDto dto)
    {
        public UpdateContactPhoneNumberDto FromRetrievedToContactPhoneNumberDto()
        {
            return new UpdateContactPhoneNumberDto
            {
                Id = dto.Id,
                Number = dto.Number,
                CountryCode = dto.CountryCode,
                PhoneNumberCategory = dto.PhoneNumberCategory,
                ContactId = dto.ContactId
            };
        }
    }

    extension(List<RetrievedContactPhoneNumberDto> dtos)
    {
        public List<UpdateContactPhoneNumberDto> FromRetrievedToUpdatePhoneNumberDtoList()
        {
            return dtos
                .Select(p => p.FromRetrievedToContactPhoneNumberDto())
                .ToList();
        }
    }
}
