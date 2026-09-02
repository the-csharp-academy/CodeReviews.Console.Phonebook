using PhoneBook.Models;

namespace Phone_Book.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}