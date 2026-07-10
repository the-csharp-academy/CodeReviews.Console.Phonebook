using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Data;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=PhoneBook.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(category => category.Name)
            .IsUnique();

        modelBuilder.Entity<Contact>().HasData(
            new Contact { Id = 1, Name = "John Doe", PhoneNumber = "+1-555-0101", EmailAddress = "john@example.com" },
            new Contact { Id = 2, Name = "Jane Smith", PhoneNumber = "+1-555-0102", EmailAddress = "jane@example.com" },
            new Contact { Id = 3, Name = "Bob Johnson", PhoneNumber = "+1-555-0103" }
        );
    }

}
