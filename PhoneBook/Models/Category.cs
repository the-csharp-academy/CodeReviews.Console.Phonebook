using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    // Navigation property: one category → many contacts
    public List<Contact> Contacts { get; set; } = [];
}