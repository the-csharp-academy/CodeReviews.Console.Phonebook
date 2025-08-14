using System.Text;

namespace PhoneBook.Entities;

public class Contact
{
    public Contact(string name, string email, string phoneNumber)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public int CategoryId { get; set; }
    public ContactsCategory Category { get; set; }
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(new string('-', 30));
        sb.AppendLine($"ID: {Id}\nName: {Name}\nE-mail: {Email}\nPhone Number: {PhoneNumber}\nCategory: {Category?.Name}");
        sb.AppendLine(new string('-', 30));
        return sb.ToString().TrimEnd();
    }
    public void SetCategory(ContactsCategory category)
    {
        Category = category;
        CategoryId = category.Id;
    }
}
