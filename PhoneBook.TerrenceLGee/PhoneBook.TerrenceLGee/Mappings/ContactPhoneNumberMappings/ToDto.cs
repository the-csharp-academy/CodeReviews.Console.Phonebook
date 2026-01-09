using PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;
using PhoneBook.TerrenceLGee.Entities;

namespace PhoneBook.TerrenceLGee.Mappings.ContactPhoneNumberMappings;

public static class ToDto
{
    extension(ContactPhoneNumber phoneNumber)
    {
        public RetrievedContactPhoneNumberDto ToRetrievedContactPhoneNumberDto()
        {
            return new RetrievedContactPhoneNumberDto
            {
                Id = phoneNumber.Id,
                Number = phoneNumber.Number,
                CountryCode = phoneNumber.CountryCode,
                PhoneNumberCategory = phoneNumber.PhoneNumberCategory,
                ContactId = phoneNumber.ContactId
            };
        }
    }
}
