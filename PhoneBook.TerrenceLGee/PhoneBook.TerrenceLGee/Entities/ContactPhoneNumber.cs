using PhoneBook.TerrenceLGee.Entities.Enums;

namespace PhoneBook.TerrenceLGee.Entities;

public class ContactPhoneNumber
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public CountryCode CountryCode { get; set; }
    public PhoneNumberCategory PhoneNumberCategory { get; set; }
    public int ContactId { get; set; }
    public Contact Contact { get; set; } = null!;
}
