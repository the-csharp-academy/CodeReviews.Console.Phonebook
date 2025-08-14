using Microsoft.EntityFrameworkCore;
using PhoneBook.Entities;
using System.Configuration;

namespace PhoneBook.DBContext;

public class ContactsDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactsCategory> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
    }
}
