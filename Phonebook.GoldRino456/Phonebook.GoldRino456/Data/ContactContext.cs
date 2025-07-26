using Microsoft.EntityFrameworkCore;

namespace PhoneBook.GoldRino456.Data;
public class ContactContext : DbContext
{
    public DbSet<ContactEntry> Contacts { get; set; }
    public DbSet<ContactCategory> ContactCategories { get; set; }

    public readonly string _connectionString;

    public ContactContext(string connectionString)
    {
        if(string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("ConnectionString was null or empty", nameof(connectionString));
        }
        
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connectionString);
}
