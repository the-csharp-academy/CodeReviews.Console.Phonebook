using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Model;

internal class PhoneBookDetails
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string EmailAddress { get; set; }
}
