using Microsoft.Extensions.Configuration;
using Phonebook.EntityFramework.Services;
using Spectre.Console;


var isAppRunning = true;
while (isAppRunning)
{
    var option = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
        .Title("What would you like to do?")
        .AddChoices(
            MenuOptions.AddContact,
            MenuOptions.ViewContact,
            MenuOptions.UpdateContact,
            MenuOptions.DeleteContact,
            MenuOptions.ViewAllContacts,
            MenuOptions.SendEmail,
            MenuOptions.ManageCategories));

    switch (option)
    {
        case MenuOptions.AddContact:
            ContactService.InsertContact();
            break;
        case MenuOptions.ViewContact:
            ContactService.GetContact();
            break;
        case MenuOptions.UpdateContact:
            ContactService.UpdateContact();
            break;
        case MenuOptions.DeleteContact:
            ContactService.DeleteContact();
            break;
        case MenuOptions.ViewAllContacts:
            ContactService.GetContacts();
            break;
        case MenuOptions.SendEmail:
            ContactService.SendEmailAsync();
            break;
        case MenuOptions.ManageCategories:
            CategoryMenu();
            break;
    }
}
static void CategoryMenu()
{
    var isCategoryMenuRunning = true;

    while (isCategoryMenuRunning)
    {
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuCategoriesOptions>()
            .Title("Category Menu")
            .AddChoices(
                MenuCategoriesOptions.AddCategory,
                MenuCategoriesOptions.ViewCategory,
                MenuCategoriesOptions.UpdateCategory,
                MenuCategoriesOptions.DeleteCategory,
                MenuCategoriesOptions.ViewAllCategories,
                MenuCategoriesOptions.BackToMainMenu));

        switch (option)
        {
            case MenuCategoriesOptions.AddCategory:
                CategoryService.InsertCategory();
                break;

            case MenuCategoriesOptions.ViewCategory:
                CategoryService.GetCategory();
                break;

            case MenuCategoriesOptions.UpdateCategory:
                CategoryService.UpdateCategory();
                break;

            case MenuCategoriesOptions.DeleteCategory:
                CategoryService.DeleteCategory();
                break;

            case MenuCategoriesOptions.ViewAllCategories:
                CategoryService.GetCategories();
                break;

            case MenuCategoriesOptions.BackToMainMenu:
                isCategoryMenuRunning = false;
                break;
        }
    }
}

enum MenuOptions
{
    ViewContact,
    AddContact,
    UpdateContact,
    DeleteContact,
    ViewAllContacts,
    SendEmail,
    ManageCategories,
    Quit   

}

enum MenuCategoriesOptions
{
    AddCategory,
    ViewCategory,
    UpdateCategory,
    DeleteCategory,
    ViewAllCategories,
    BackToMainMenu
}
