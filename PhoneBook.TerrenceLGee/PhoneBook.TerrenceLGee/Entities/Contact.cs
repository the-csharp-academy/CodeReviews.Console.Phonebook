using PhoneBook.TerrenceLGee.Entities.Enums;

namespace PhoneBook.TerrenceLGee.Entities;

public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public ICollection<ContactPhoneNumber> PhoneNumbers { get; set; } = [];
    public ContactCategory Category { get; set; }
    public string? Notes { get; set; }
    public string? Suffix { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? NickName { get; set; }
}
