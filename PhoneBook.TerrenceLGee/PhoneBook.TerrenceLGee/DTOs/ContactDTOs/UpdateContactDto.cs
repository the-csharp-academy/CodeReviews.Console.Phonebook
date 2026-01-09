using PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;
using PhoneBook.TerrenceLGee.Entities.Enums;

namespace PhoneBook.TerrenceLGee.DTOs.ContactDTOs;

public class UpdateContactDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public ICollection<UpdateContactPhoneNumberDto> PhoneNumbers { get; set; } = [];
    public ContactCategory Category { get; set; }
    public string? Notes { get; set; }
    public string? Suffix { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? NickName { get; set; }
}
