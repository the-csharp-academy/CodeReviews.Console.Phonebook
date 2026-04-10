using PhoneBook.davetn657.Controllers;
using PhoneBook.davetn657.Data;
using PhoneBook.davetn657.Views;

namespace PhoneBook.davetn657
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var phoneBook = new PhoneBookContext();
            var optionUtils = new OptionUtils();
            var validation = new Validation();
            var uI = new UserInterface(phoneBook, optionUtils, validation);

            uI.Start();
        }
    }
}
