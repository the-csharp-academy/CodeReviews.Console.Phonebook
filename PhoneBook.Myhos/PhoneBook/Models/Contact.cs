using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Models;

[Index(nameof(Name), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
internal class Contact
{
    public int ContactId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}