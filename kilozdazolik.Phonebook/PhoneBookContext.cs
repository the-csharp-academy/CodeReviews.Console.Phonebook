using kilozdazolik.Phonebook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace kilozdazolik.Phonebook;

public class PhoneBookContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Context Settings
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Contacts)
            .HasForeignKey(p => p.CategoryId);


        modelBuilder.Entity<Category>().HasData(new List<Category>
        {
            new Category { CategoryId = 1, Name = "Default" },
            new Category { CategoryId = 2, Name = "Friends" },
            new Category { CategoryId = 3, Name = "Family" },
            new Category { CategoryId = 4, Name = "Work" }
        });

        modelBuilder.Entity<Contact>().HasData(new List<Contact>
        {
            new Contact
            {
                ContactId = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "21545678396",
                CategoryId = 1
            },
            new Contact
            {
                ContactId = 2,
                Name =  "John Smith",
                Email = "john.smith@example.com",
                PhoneNumber = "21545678996",
                CategoryId = 2
            },
            new Contact
            {
                ContactId = 3,
                Name =  "John Jones",
                Email = "john.jones@example.com",
                PhoneNumber = "21545678912",
                CategoryId = 1
            },
            new Contact
            {
                ContactId = 4,
                Name =  "Adam Forest",
                Email = "adam.forest@example.com",
                PhoneNumber = "21515271912",
                CategoryId = 3
            }
        });
    }
}