using PhoneBook.Controllers;
using PhoneBook.DBContext;

namespace PhoneBook;

public class Program
{
    static void Main()
    {
        var dbContext = new ContactsDbContext();
        var controller = new ContactsController(dbContext);
    }
}
