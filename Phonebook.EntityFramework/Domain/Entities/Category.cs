namespace Phonebook.EntityFramework.Domain.Entities;

internal class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Contact> Contacts { get; set; } = new();
}
