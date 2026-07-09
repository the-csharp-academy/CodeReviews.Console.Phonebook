using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.Models;

namespace PhoneBook;

internal class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Category)
            .WithMany(cc => cc.Contacts)
            .HasForeignKey(c => c.CategoryId);

        modelBuilder.Entity<Contact>()
            .HasData(new List<Contact>
            {
                new Contact 
                {
                    ContactId = 1,
                    Name = "Miguel",
                    Email = "test1@gmail.com",
                    PhoneNumber = "+571234567890",
                    CategoryId = 1,
                },
                new Contact 
                {
                    ContactId = 2,
                    Name = "Angel",
                    Email = "test2@gmail.com",
                    PhoneNumber = "+571234567890",
                    CategoryId = 1,
                },
                new Contact 
                {
                    ContactId = 3,
                    Name = "David",
                    Email = "test3@gmail.com",
                    PhoneNumber = "+571234567890",
                    CategoryId = 2,
                },
                new Contact 
                {
                    ContactId = 4,
                    Name = "Luis",
                    Email = "test4@gmail.com",
                    PhoneNumber = "+571234567890",
                    CategoryId = 2,
                },
                new Contact 
                {
                    ContactId = 5,
                    Name = "Cecilia",
                    Email = "test5@gmail.com",
                    PhoneNumber = "+571234567890",
                    CategoryId = 3,
                },
                new Contact 
                {
                    ContactId = 6,
                    Name = "German",
                    Email = "test6@gmail.com",
                    PhoneNumber = "+571234567890",
                    CategoryId = 3,
                }
            });

        modelBuilder.Entity<Category>()
            .HasData(new List<Category> 
            {
                new Category
                {
                    CategoryId = 1,
                    Name = "Family"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Work"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Friends"
                }
            });
    }
}