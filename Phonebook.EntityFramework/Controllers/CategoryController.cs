using Phonebook.EntityFramework.Domain.Entities;

namespace Phonebook.EntityFramework.Controllers;

internal class CategoryController
{
    internal static void AddCategory(string name)
    {

        using var db = new ContactContext();
        db.Add(new Category { Name = name});
        db.SaveChanges();

    }
    internal static Category GetCategoryById(int id)
    {
        using var db = new ContactContext();
        var category = db.Categories.SingleOrDefault(x => x.Id == id);

        return category;
    }

    internal static void DeleteCategory(Category category)
    {
        using var db = new ContactContext();
        db.Remove(category);
        db.SaveChanges();
    }

    internal static void UpdateCategory(Category category)
    {
        using var db = new ContactContext();
        db.Update(category);
        db.SaveChanges();
    }

    internal static List<Category> GetCategories()
    {
        using var db = new ContactContext();
        var categories = db.Categories.ToList();
        return categories;
    }
}
