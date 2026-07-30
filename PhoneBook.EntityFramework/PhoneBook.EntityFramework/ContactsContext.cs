using Microsoft.EntityFrameworkCore;
namespace PhoneBook.EntityFramework;


internal class ContactsContext : DbContext 
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>

    optionsBuilder.UseSqlite($"Data source = contacts.db");
    
}
