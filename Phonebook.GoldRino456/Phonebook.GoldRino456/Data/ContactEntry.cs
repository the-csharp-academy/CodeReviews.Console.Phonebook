namespace PhoneBook.GoldRino456.Data;
public class ContactEntry
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ContactCategory? Category { get; set; }
}
