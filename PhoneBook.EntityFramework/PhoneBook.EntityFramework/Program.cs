using PhoneBook.EntityFramework;
using Spectre.Console;

var isRunnning = true;

while (isRunnning)
{
    var option = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
        .Title("What would you like to do?")
        .AddChoices(
        MenuOptions.AddContact,
        MenuOptions.DeleteContact,
        MenuOptions.UpdateContact,
        MenuOptions.ViewAllContacts,
        MenuOptions.ViewContact,
        MenuOptions.Quit
            ));


    switch (option)
    {
        case MenuOptions.AddContact:
            ContactService.InsertContact();
            break;

        case MenuOptions.DeleteContact:
            ContactService.DeleteContact();
            break;

        case MenuOptions.UpdateContact:
            ContactService.UpdateContact();
            break;

        case MenuOptions.ViewContact:
            ContactService.ViewContacts();
            break;

        case MenuOptions.ViewAllContacts:
            ContactService.ViewAllContacts();
            break;

            case MenuOptions.Quit:
            Environment.Exit(0);
                break;
    }
}
enum MenuOptions
{
   AddContact,
   DeleteContact,
   UpdateContact,
   ViewContact,
   ViewAllContacts,
   Quit

}