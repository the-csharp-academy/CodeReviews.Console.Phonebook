namespace Phone_Book.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}