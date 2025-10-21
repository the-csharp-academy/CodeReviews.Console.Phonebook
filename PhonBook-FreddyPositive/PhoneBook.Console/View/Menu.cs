using PhoneBook.Services;
using Spectre.Console;
using static PhoneBook.Enums;

namespace PhoneBook.View;

internal class MainMenu
{
    public static void PhoneBookUserMenu()
    {
        bool IsAppRun = true;

        while (IsAppRun)
        {
            Console.Clear();
            Console.WriteLine("------ Welcome To Phone Book App ------");
            var options = AnsiConsole.Prompt(
            new SelectionPrompt<PhoneBookMenu>()
                .Title("What would you like to do?")
                .AddChoices(PhoneBookMenu.AddContact,
                            PhoneBookMenu.ViewAllContacts,
                            PhoneBookMenu.SearchContact,
                            PhoneBookMenu.UpdateContact,
                            PhoneBookMenu.DeleteContact,
                            PhoneBookMenu.Exit
                          ));

            switch (options)
            {
                case PhoneBookMenu.AddContact:
                    PhoneBookService.AddContact();
                    break;
                case PhoneBookMenu.ViewAllContacts:
                    PhoneBookService.ViewAllContacts(true);
                    break;
                case PhoneBookMenu.SearchContact:
                    PhoneBookService.SearchContact();
                    break;
                case PhoneBookMenu.UpdateContact:
                    PhoneBookService.UpdateContact();
                    break;
                case PhoneBookMenu.DeleteContact:
                    PhoneBookService.DeleteContact();
                    break;
                case PhoneBookMenu.Exit:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
