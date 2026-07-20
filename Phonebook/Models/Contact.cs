using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models 
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = "";
        [StringLength(100)]
        public string? Email { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = "";
        [StringLength(100)]
        public string? Category { get; set; }
    }
}
