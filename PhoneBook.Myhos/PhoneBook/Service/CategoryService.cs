using PhoneBook.Controller;
using PhoneBook.Menus;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Service;

internal class CategoryService
{
    internal static void InsertCategory()
    {
        Category category = new Category();

        category.Name = ValidateInput.ValidateCategoryName("Category's name: ");

        if (CategoryController.AddCategory(category)) 
        {
            AnsiConsole.MarkupLine("[#6EEB83]Category created successfully.[/]");
        }
        else 
        {
            AnsiConsole.MarkupLine("[red]Failed to create contact.[/]");
        }

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();
    }

    internal static void ViewCategory()
    {
        Category category = GetCategoryOptionInput();

        if(category == null) 
        {
            AnsiConsole.MarkupLine("[#6EEB83]There are no categories[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        }

        UserInterface.ShowCategory(category);
    }

    internal static void ViewAllCategories()
    {
        List<Category> categories = CategoryController.GetAllCategories();

        if(categories.Count == 0) 
        {
            AnsiConsole.MarkupLine("[#FF934F]There are no categories[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        }

        UserInterface.ShowCategoriesTable(categories);
    }

    internal static void DeleteCategory()
    {
        Category category = GetCategoryOptionInput();

        if (category == null)
        {
            AnsiConsole.MarkupLine("[#FF934F]There are no categories[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        }

        if (CategoryController.DeleteCategory(category)) 
        {
            AnsiConsole.MarkupLine("[#6EEB83]Category deleted successfully.[/]");
        }
        else 
        {
            AnsiConsole.MarkupLine("[red]Failed to delete category.[/]");
        }

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();
    }

    internal static void UpdateCategory()
    {
        Category category = GetCategoryOptionInput();

        if (category == null)
        {
            AnsiConsole.MarkupLine("[#FF934F]There are no categories[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        }

        category.Name = AnsiConsole.Confirm("Update Name: ")
            ? ValidateInput.ValidateName("Category's name: ")
            : category.Name;

        if (CategoryController.UpdateCategory(category))
        {
            AnsiConsole.MarkupLine("[#6EEB83]Category updated successfully.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed to update category.[/]");
        }

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();
    }

    internal static Category GetCategoryOptionInput()
    {
        var categories = CategoryController.GetAllCategories();

        if (categories.Count == 0)
        {
            return null;
        }

        string[] categoriesNames = categories.Select(c => c.Name).ToArray();

        var options = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a category")
            .AddChoices(categoriesNames));

        int id = categories.Single(c => c.Name == options).CategoryId;
        Category category = CategoryController.GetCategoryById(id);

        return category;
    }
}