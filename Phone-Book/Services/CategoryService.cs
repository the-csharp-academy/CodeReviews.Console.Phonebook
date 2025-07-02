using Phone_Book.Controllers;
using Phone_Book.Models;
using Phone_Book.Views;
using Spectre.Console;

namespace Phone_Book.Services;

public class CategoryService
{
    internal static void InsertCategory()
    {
        var category = new Category();

        category.CategoryName = AnsiConsole.Ask<string>("Enter Category Name:");

        CategoryController.AddCategory(category);
    }

    internal static void UpdateCategory()
    {
        var category = GetCategoryByName();

        category.CategoryName = AnsiConsole.Ask<string>("Enter New Category Name:");

        CategoryController.UpdateCategory(category);
    }

    internal static void DeleteCategory()
    {
        var category = GetCategoryByName();

        CategoryController.DeleteCategory(category);
    }

    internal static void GetAllCategories()
    {
        var categories = CategoryController.GetAllCategories();

        UserInterface.ShowCategoriesTable(categories);
    }

    internal static Category GetCategoryByName()
    {
        var categories = CategoryController.GetAllCategories();
        var categoriesArray = categories.Select(c => c.CategoryName).ToArray();
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a category:")
                .PageSize(10)
                .AddChoices(categoriesArray)
        );
        var selectedCategory = categories.FirstOrDefault(c => c.CategoryName == option);
        return selectedCategory;
    }
}