using System.ComponentModel.DataAnnotations;

namespace PhoneGallery.DreamFXX.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
    public string Name { get; set; }

    public List<Contact> Contacts { get; set; } = new();
}

