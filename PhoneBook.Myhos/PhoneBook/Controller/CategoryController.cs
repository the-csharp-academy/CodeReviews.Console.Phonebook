using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Controller;

internal class CategoryController
{
    internal static bool AddCategory(Category category)
    {
        try
        {
            using var db = new PhoneBookContext();
            db.Add(category);
            return db.SaveChanges() > 0;
        }
        catch(Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error adding contact:[/] {ex.Message}");
            return false;
        }
    }

    internal static Category GetCategoryById(int id) 
    {
        try
        {
            using var db = new PhoneBookContext();
            var category = db.Categories
                .SingleOrDefault(x => x.CategoryId == id);

            return category;
        }
        catch (Exception ex) 
        {
            AnsiConsole.MarkupLine($"[red]Error loading contact:[/] {ex.Message}");
            return null;
        }
    }

    internal static List<Category> GetAllCategories() 
    {
        try
        {
            using var db = new PhoneBookContext();

            List<Category> categories = db.Categories
                .Include(c => c.Contacts)
                .ToList();

            return categories;
        }
        catch (Exception ex) 
        {
            AnsiConsole.MarkupLine($"[red]Error loading contacts:[/] {ex.Message}");
            return [];
        }
    }

    internal static bool DeleteCategory(Category category)
    {
        try
        {
            using var db = new PhoneBookContext();
            db.Remove(category);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex) 
        {
            AnsiConsole.MarkupLine($"[red]Error delete contact:[/] {ex.Message}");
            return false;
        }
    }

    internal static bool UpdateCategory(Category category) 
    {
        try
        {
            using var db = new PhoneBookContext();

            db.Update(category);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex) 
        {
            AnsiConsole.MarkupLine($"[red]Error updating contact:[/] {ex.Message}");
            return false;
        }
    }

    internal static bool NameExist(string name) 
    {
        using var db = new PhoneBookContext();

        return db.Categories.Any(c => c.Name == name);
    }
}