namespace PhoneBook.GoldRino456.Data;
public class ContactCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ContactEntry> Contacts { get; set; }
}
