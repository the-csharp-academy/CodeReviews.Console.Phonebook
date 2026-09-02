using Microsoft.EntityFrameworkCore;
using Phone_Book.Models;
using Spectre.Console;

namespace PhoneBook
{
    internal class CategoryController
    {
        public void CreateCategory(Category userCategory)
        {
            using var db = new ContactContext();
            db.Categories.Add(userCategory);

            SaveChanges(db);
        }

        public List<Category> GetAllCategories()
        {
            using var db = new ContactContext();
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

        public void UpdateCategory(Category userCategory)
        {
            using var db = new ContactContext();
            db.Categories.Update(userCategory);

            SaveChanges(db);
        }

        public void DeleteCategory(Category userCategory)
        {
            using var db = new ContactContext();
            db.Categories.Remove(userCategory);

            SaveChanges(db);
        }

        public void SaveChanges(ContactContext db)
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                AnsiConsole.MarkupLine("[Red]ERROR [/]Couldn't save changes");
                Environment.Exit(0);
            }
            catch (DbUpdateException)
            {
                AnsiConsole.MarkupLine("[Red]ERROR [/]Couldn't save changes");
                Environment.Exit(0);
            }
        }
    }
}