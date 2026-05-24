using Phonebook.EntityFramework.Controllers;
using Phonebook.EntityFramework.Domain.Entities;
using Spectre.Console;

namespace Phonebook.EntityFramework.Services;

internal class CategoryService
{
    static internal void GetCategories()
    {
        var categories = CategoryController.GetCategories();
        UserInterface.ShowCategoryTable(categories);

    }

    static internal void GetCategory()
    {
        var category = UserInterface.GetCategoryOptionInput();
        UserInterface.ShowCategory(category);
    }    

    static internal void InsertCategory()
    {
        var name = AnsiConsole.Ask<string>("What is the category's name?");
        CategoryController.AddCategory(name);
    }

    static internal void DeleteCategory()
    {
        var category = UserInterface.GetCategoryOptionInput();
        CategoryController.DeleteCategory(category);
    }

    internal static void UpdateCategory()
    {
        var category = UserInterface.GetCategoryOptionInput();
        var name = AnsiConsole.Ask<string>("What is the category's new name?");
        CategoryController.UpdateCategory(category);
    }
}
