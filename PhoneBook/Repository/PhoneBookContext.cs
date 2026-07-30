using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Repository;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<PhoneNumberDetail> PhoneNumbers { get; set; }
    public DbSet<EmailDetail> Emails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=PhoneBookDb;Trusted_Connection=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().HasData(
            new Contact
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                OrganizationName = "Microsoft",
                JobTitle = "Software Engineer",
                Notes = "College friend",
                CreatedAt = new DateTime(2026, 1, 1)
            },
            new Contact
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                OrganizationName = "Google",
                JobTitle = "Product Manager",
                Notes = "Met at conference",
                CreatedAt = new DateTime(2026, 1, 2)
            },
            new Contact
            {
                Id = 3,
                FirstName = "David",
                LastName = "Wilson",
                OrganizationName = "Amazon",
                JobTitle = "Cloud Architect",
                Notes = "Work contact",
                CreatedAt = new DateTime(2026, 1, 3)
            }
        );

        modelBuilder.Entity<PhoneNumberDetail>().HasData(
            new PhoneNumberDetail
            {
                Id = 1,
                Number = "9876543210",
                Label = LabelType.Mobile,
                ContactId = 1,
                CreatedAt = new DateTime(2026, 1, 1)
            },
            new PhoneNumberDetail
            {
                Id = 2,
                Number = "9123456789",
                Label = LabelType.Work,
                ContactId = 2,
                CreatedAt = new DateTime(2026, 1, 2)
            },
            new PhoneNumberDetail
            {
                Id = 3,
                Number = "9988776655",
                Label = LabelType.Mobile,
                ContactId = 3,
                CreatedAt = new DateTime(2026, 1, 3)
            }
        );

        modelBuilder.Entity<EmailDetail>().HasData(
            new EmailDetail
            {
                Id = 1,
                EmailAddress = "john.doe@example.com",
                Label = LabelType.Work,
                ContactId = 1,
                CreatedAt = new DateTime(2026, 1, 1)
            },
            new EmailDetail
            {
                Id = 2,
                EmailAddress = "jane.smith@example.com",
                Label = LabelType.Work,
                ContactId = 2,
                CreatedAt = new DateTime(2026, 1, 2)
            },
            new EmailDetail
            {
                Id = 3,
                EmailAddress = "david.wilson@example.com",
                Label = LabelType.Home,
                ContactId = 3,
                CreatedAt = new DateTime(2026, 1, 3)
            }
        );
    }
}

