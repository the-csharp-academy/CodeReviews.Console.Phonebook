namespace PhoneBook.Entities
{
    public class ContactsCategory
    {
        public ContactsCategory(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
