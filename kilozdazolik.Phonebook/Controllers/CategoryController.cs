using kilozdazolik.Phonebook.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace kilozdazolik.Phonebook.Controllers;

public class CategoryController
{
      internal static void AddCategory(Category category)
    {
        using var db = new PhoneBookContext();
        db.Add(category);
        db.SaveChanges();
    }
      
    internal static void DeleteCategory(Category category)
    {
        using var db = new PhoneBookContext();
        if (category.CategoryId == 1)
        {
            AnsiConsole.MarkupLine("[red]You can't delete this category![/]");
        }
        else
        {
            db.Remove(category);
            db.SaveChanges();
        }
    }

    internal static List<Category> GetCategories()
    {
        using var db = new PhoneBookContext();
        var categories = db.Categories.Include(x => x.Contacts).ToList();
        return categories;
    }
    
    
    internal static void UpdateCategory(Category category)
    { 
        using var db = new PhoneBookContext();
        if (category.CategoryId == 1)
        {
            AnsiConsole.MarkupLine("[red]You can't update this category![/]");
        }
        else
        {
            db.Update(category);
            db.SaveChanges();
        }
    }
}