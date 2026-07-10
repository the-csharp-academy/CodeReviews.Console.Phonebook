using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models;

public class Contact
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(100)]
    public string? EmailAddress { get; set; }

    public int? CategoryId { get; set; }         // Foreign key (nullable = optional)
    public Category? Category { get; set; }       // Navigation property
}