using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Phonebook.Model;

internal class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public string ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SQLConnectionString")["connectionString"];
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        List<Category> categories = new List<Category>
        {
            new Category {CategoryName = "Family"},
            new Category {CategoryName = "Friends"},
            new Category {CategoryName = "Coworkers"}
        };
        List<Contact> contacts = new List<Contact>
        {
            new Contact {ContactName = "John Doe", PhoneNumber = "123345412", Email = "John.Doe@example.com", CategoryName = "Family"},
            new Contact {ContactName = "Mother Doe", PhoneNumber = "4124114123", Email = "Mother.Doe@example.com", CategoryName = "Family"},
            new Contact {ContactName = "Father Doe", PhoneNumber = "6262153515", Email = "Father.Doe@example.com", CategoryName = "Family"},
            new Contact {ContactName = "Friend Doe", PhoneNumber = "124124124", Email = "Friend.Doe@example.com", CategoryName = "Friends"},
            new Contact {ContactName = "Another Doe", PhoneNumber = "99878989", Email = "Another.Doe@example.com", CategoryName = "Friends"},
            new Contact {ContactName = "Coworker Doe", PhoneNumber = "99986544", Email = "Coworker.Doe@gmail.com", CategoryName = "Coworkers"},
            new Contact {ContactName = "Another Coworker", PhoneNumber = "98888712", Email = "Coworker2@gmail.com", CategoryName = "Coworkers"},
            new Contact {ContactName = "Random Girl", PhoneNumber = "7841374", Email = "random@hotmail.com"},
            new Contact {ContactName = "Random Guy", PhoneNumber = "481208478", Email = "random123@yahoo.com"},
        };
        optionsBuilder.UseSqlServer(ConnectionString)
        .UseSeeding((context, _) =>
        {
            foreach(var category in categories)
            {
                var testCategory = context.Set<Category>().FirstOrDefault(b => b.CategoryId == category.CategoryId);
                if(testCategory == null)
                {
                    context.Set<Category>().Add(category);
                    context.SaveChanges();
                }
            }
            foreach(var contact in contacts)
            {
                var testContact = context.Set<Contact>().FirstOrDefault(b => b.ContactId == contact.ContactId);
                if(testContact == null)
                {
                    context.Set<Contact>().Add(contact);
                    context.SaveChanges();
                }
            }

        })
        .UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            foreach(var category in categories)
            {
                var testCategory = await context.Set<Category>().FirstOrDefaultAsync(b => b.CategoryId == category.CategoryId, cancellationToken);
                if (testCategory == null)
                {
                    context.Set<Category>().Add(category);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
            foreach(var contact in contacts)
            {
                var testContact = await context.Set<Contact>().FirstOrDefaultAsync(b => b.ContactId == contact.ContactId, cancellationToken);
                if (testContact == null)
                {
                    context.Set<Contact>().Add(contact);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        });
    }
}
