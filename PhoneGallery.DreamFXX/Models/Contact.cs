using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneGallery.DreamFXX.Models;

public class Contact
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Contact name is required. Please, enter the name of the contact.")]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Email address is required. Please enter a valid email address. Example: nickname@domainName.com")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required. Please enter a valid phone number")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string PhoneNumber { get; set; }

    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

