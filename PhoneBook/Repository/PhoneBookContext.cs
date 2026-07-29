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
}
