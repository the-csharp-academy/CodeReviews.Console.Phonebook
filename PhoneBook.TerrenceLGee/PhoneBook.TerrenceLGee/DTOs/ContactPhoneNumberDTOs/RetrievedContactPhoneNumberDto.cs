using PhoneBook.TerrenceLGee.Entities.Enums;

namespace PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;

public class RetrievedContactPhoneNumberDto
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public CountryCode CountryCode { get; set; }
    public PhoneNumberCategory PhoneNumberCategory { get; set; }
    public int ContactId { get; set; }
}
