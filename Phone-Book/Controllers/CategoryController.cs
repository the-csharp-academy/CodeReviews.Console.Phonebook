using Phone_Book.Data;
using Phone_Book.Models;

namespace Phone_Book.Controllers;

public class CategoryController
{
    internal static void AddCategory(Category category)
    {
        using var db = new ContactContext();
        db.Add(category);
        db.SaveChanges();
    }

    internal static void UpdateCategory(Category category)
    {
        using var db = new ContactContext();
        db.Update(category);
        db.SaveChanges();
    }

    internal static void DeleteCategory(Category category)
    {
        using var db = new ContactContext();
        db.Remove(category);
        db.SaveChanges();
    }

    internal static List<Category> GetAllCategories()
    {
        using var db = new ContactContext();
        var categories = db.Categories.ToList();
        return categories;
    }
}