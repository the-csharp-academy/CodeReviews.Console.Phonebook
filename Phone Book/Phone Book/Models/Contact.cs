using Phone_Book.Models;

namespace PhoneBook.Models
{
    public class Contact
    {
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
    }
}