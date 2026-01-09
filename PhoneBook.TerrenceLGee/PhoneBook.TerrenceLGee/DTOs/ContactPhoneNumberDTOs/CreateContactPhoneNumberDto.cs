using PhoneBook.TerrenceLGee.Entities.Enums;

namespace PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;

public class CreateContactPhoneNumberDto
{
    public string Number { get; set; } = string.Empty;
    public CountryCode CountryCode { get; set; }
    public PhoneNumberCategory PhoneNumberCategory { get; set; }
}
