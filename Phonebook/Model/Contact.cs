using Microsoft.EntityFrameworkCore;

namespace Phonebook.Model;

[Index(nameof(PhoneNumber), IsUnique = true)]
[Index(nameof(ContactName), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
internal class Contact
{
    public int ContactId { set; get; }
    public required string ContactName { set; get; }
    public required string PhoneNumber { set; get; }
    public required string Email { set; get; }
    public string? CategoryName { set; get; } = null;
}
