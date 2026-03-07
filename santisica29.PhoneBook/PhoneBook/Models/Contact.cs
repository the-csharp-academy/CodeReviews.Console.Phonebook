using static PhoneBook.Models.Enums;

namespace PhoneBook.Models;
internal class Contact
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required UserCategories Category { get; set; }
}
