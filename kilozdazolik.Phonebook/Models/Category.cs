namespace kilozdazolik.Phonebook.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public List<Contact> Contacts { get; set; }
}