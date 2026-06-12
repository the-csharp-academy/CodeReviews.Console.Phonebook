using Phonebook.Controller;

namespace Phonebook;

internal class UserInterface
{
    internal void MainMenu()
    {
        bool isCloseApp = false;

        while (!isCloseApp)
        {
            Console.Clear();
            Console.WriteLine("---Welcome to Phonebook---");
            Console.WriteLine("1 - View all contacts");
            Console.WriteLine("2 - Add new contact");
            Console.WriteLine("3 - Delete contact");
            Console.WriteLine("4 - Update contact");
            Console.WriteLine("5 - Find contact");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("Please select an option: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    isCloseApp = true;
                    break;
                case "1":
                    ContactController.ViewContacts();
                    break;
                case "2":
                    ContactController.NewContact();
                    break;
                case "3":
                    ContactController.DeleteContact();
                    break;
                case "4":
                    ContactController.UpdateContact();
                    break;
                case "5":
                    ContactController.FindContact();
                    break;
            }
        }
    }   
}
