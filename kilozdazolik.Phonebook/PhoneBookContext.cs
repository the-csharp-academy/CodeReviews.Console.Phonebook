using kilozdazolik.Phonebook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace kilozdazolik.Phonebook;

public class PhoneBookContext : DbContext
{
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
        modelBuilder.Entity<Contact>().Property(c => c.Name).IsRequired();
        
        modelBuilder.Entity<Contact>().HasData(new List<Contact>
        {
            new Contact
            {
                ContactId = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "21545678396"
            },
            new Contact
            {
                ContactId = 2,
                Name =  "John Smith",
                Email = "john.smith@example.com",
                PhoneNumber = "21545678996"
            },
            new Contact
            {
                ContactId = 3,
                Name =  "John Jones",
                Email = "john.jones@example.com",
                PhoneNumber = "21545678912"
            }
        });
    }
}