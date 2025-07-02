using Microsoft.EntityFrameworkCore;
using Phone_Book.Models;

namespace Phone_Book.Data;

internal class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=phonebook;Trusted_Connection=True"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Contact>()
            .HasOne(co => co.Category)
            .WithMany(ca => ca.Contacts)
            .HasForeignKey(co => co.CategoryId);

        modelBuilder
            .Entity<Category>()
            .HasData(
                new List<Category>
                {
                    new() { CategoryId = 1, CategoryName = "Family" },
                    new() { CategoryId = 2, CategoryName = "Friends" },
                    new() { CategoryId = 3, CategoryName = "Work" }
                }
            );

        modelBuilder
            .Entity<Contact>()
            .HasData(
                new List<Contact>
                {
                    new()
                    {
                        Id = 1,
                        Name = "Julienne Husher",
                        Email = "jhusher0@ted.com",
                        Phone = "128-187-6389",
                        CategoryId = 1
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Farlay Kemball",
                        Email = "fkemball1@webmd.com",
                        Phone = "178-994-6729",
                        CategoryId = 1
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Golda Garahan",
                        Email = "ggarahan2@51.la",
                        Phone = "468-314-0933",
                        CategoryId = 1
                    },
                    new()
                    {
                        Id = 4,
                        Name = "Lisha MacCague",
                        Email = "lmaccague3@w3.org",
                        Phone = "999-787-9493",
                        CategoryId = 2
                    },
                    new()
                    {
                        Id = 5,
                        Name = "Nikki Bockin",
                        Email = "nbockin4@taobao.com",
                        Phone = "847-451-1638",
                        CategoryId = 2
                    },
                    new()
                    {
                        Id = 6,
                        Name = "Jemimah Grishanov",
                        Email = "jgrishanov5@blogger.com",
                        Phone = "606-400-0266",
                        CategoryId = 2
                    },
                    new()
                    {
                        Id = 7,
                        Name = "Tersina Lettuce",
                        Email = "tlettuce6@tuttocitta.it",
                        Phone = "164-204-3869",
                        CategoryId = 3
                    },
                    new()
                    {
                        Id = 8,
                        Name = "Gradey Rampling",
                        Email = "grampling7@yellowpages.com",
                        Phone = "649-743-0045",
                        CategoryId = 3
                    },
                    new()
                    {
                        Id = 9,
                        Name = "Simonette Mollnar",
                        Email = "smollnar8@hubpages.com",
                        Phone = "120-226-6327",
                        CategoryId = 3
                    }
                }
            );
    }
}