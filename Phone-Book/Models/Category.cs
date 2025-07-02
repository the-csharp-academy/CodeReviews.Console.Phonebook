namespace Phone_Book.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<Contact> Contacts { get; set; }
}