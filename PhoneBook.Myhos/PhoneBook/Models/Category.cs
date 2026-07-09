using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Models;

[Index (nameof(Name), IsUnique = true)]
internal class Category
{
    public int CategoryId {  get; set; }
    public string Name { get; set; }
    public List<Contact> Contacts { get; set; }
}