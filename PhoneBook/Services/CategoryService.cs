using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Helpers;
using PhoneBook.Models;

namespace PhoneBook.Services;

public class CategoryService
{
    private readonly Func<PhoneBookContext> _contextFactory;

    public CategoryService(Func<PhoneBookContext>? contextFactory = null)
    {
        _contextFactory = contextFactory ?? (() => new PhoneBookContext());
    }

    public List<Category> GetAllCategories()
    {
        using var context = _contextFactory();
        return context.Categories
            .Include(category => category.Contacts)
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ToList();
    }

    public bool AddCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        category.Name = NormalizeName(category.Name);

        using var context = _contextFactory();
        if (CategoryNameExists(context, category.Name))
        {
            return false;
        }

        context.Categories.Add(category);
        context.SaveChanges();
        return true;
    }

    public bool UpdateCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        var name = NormalizeName(category.Name);

        using var context = _contextFactory();
        var existingCategory = context.Categories.Find(category.Id);
        if (existingCategory is null || CategoryNameExists(context, name, category.Id))
        {
            return false;
        }

        existingCategory.Name = name;
        context.SaveChanges();
        category.Name = name;
        return true;
    }

    public bool DeleteCategory(int id)
    {
        using var context = _contextFactory();
        var category = context.Categories
            .Include(item => item.Contacts)
            .SingleOrDefault(item => item.Id == id);

        if (category is null)
        {
            return false;
        }

        foreach (var contact in category.Contacts)
        {
            contact.CategoryId = null;
        }

        context.Categories.Remove(category);
        context.SaveChanges();
        return true;
    }

    private static bool CategoryNameExists(PhoneBookContext context, string name, int? excludedCategoryId = null)
    {
        var categories = context.Categories.AsQueryable();
        if (excludedCategoryId.HasValue)
        {
            categories = categories.Where(category => category.Id != excludedCategoryId.Value);
        }

        return categories.Any(category => category.Name.ToUpper() == name.ToUpper());
    }

    private static string NormalizeName(string name)
    {
        if (!Validation.IsValidCategoryName(name))
        {
            throw new ArgumentException("Category names must contain 1 to 50 characters.", nameof(name));
        }

        return name.Trim();
    }
}
