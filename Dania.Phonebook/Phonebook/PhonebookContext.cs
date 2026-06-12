using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phonebook.Model;

namespace Phonebook;

internal class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>().HasData(
            new Contact { Id = 1, Name = "John Doe", PhoneNumber = "+18708291967", EmailAddress = "johnDoe@gmail.com"},
            new Contact { Id = 2, Name = "John Doe", PhoneNumber = "+447457095201", EmailAddress = "john.doe@gmail.com"},
            new Contact { Id = 3, Name = "Mary Jane", PhoneNumber = "+61451146406", EmailAddress = "maryjane@gmail.com"},
            new Contact { Id = 4, Name = "Alice Green", PhoneNumber = "+13052428540", EmailAddress = "alicegreen@gmail.com"},
            new Contact { Id = 5, Name = "Bob Vance", PhoneNumber = "+447984372345", EmailAddress = "bobvance@gmail.com" }
        );
    }
}
