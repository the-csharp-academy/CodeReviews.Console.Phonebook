using kilozdazolik.Phonebook.Controllers;
using kilozdazolik.Phonebook.Models;
using Spectre.Console;

namespace kilozdazolik.Phonebook.Services;

public class CategoryService
{
    internal static void InsertCategory()
    {
        var category = new Category();
        category.Name = Helper.GetNameInput();

        CategoryController.AddCategory(category);
    }

    internal static void GetCategories()
    {
        var categories = CategoryController.GetCategories();
        UserInterface.ShowTable(categories);
    }

    internal static void DeleteCategory()
    {
        var category = GetCategoryOptionInput();

        if (Helper.ConfirmMessage("Delete", category.Name))
        {
            CategoryController.DeleteCategory(category);
        }
        else
        {
            AnsiConsole.MarkupLine("Deletion canceled.");
        }
        
        AnsiConsole.MarkupLine("[green]Press Any Key to Continue.[/]");
        Console.ReadKey();
    }

    internal static void EditCategory()
    {
        var category = GetCategoryOptionInput();
        category.Name = Helper.GetNameInput();
        CategoryController.UpdateCategory(category);
    }

    internal static Category GetCategoryOptionInput()
    {
        var categories = CategoryController.GetCategories();
        var categoriesArray = categories.Select(x => x.Name).ToArray();
        var option =
            AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose Category").AddChoices(categoriesArray));
        var category = categories.Single(x => x.Name == option);
        return category;
    }
    
}