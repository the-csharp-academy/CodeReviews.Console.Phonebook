using System.ComponentModel.DataAnnotations;

namespace PhoneBook.TerrenceLGee.Entities.Enums;

public enum UpdateProperties
{
    [Display(Name = "First Name")]
    FirstName,
    [Display(Name = "Last Name")]
    LastName,
    Email,
    [Display(Name = "Phone Numbers")]
    PhoneNumbers,
    Category,
    Notes,
    Suffix,
    [Display(Name = "Date of Birth")]
    DateOfBirth,
    Nickname
}
