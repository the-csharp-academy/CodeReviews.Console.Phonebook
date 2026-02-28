using PhoneBook.m1chael888.Controllers;
using PhoneBook.m1chael888.Views;

namespace PhoneBook.m1chael888.Infrastructure
{
    public interface IRouter
    {
        void Route(ContactController contactController, IContactView contactView);
    }
    public class Router : IRouter
    {
        public void Route(ContactController contactController, IContactView contactView)
        {
            while (true)
            {
                contactController.HandleMainMenu();
            }
        }
    }
}