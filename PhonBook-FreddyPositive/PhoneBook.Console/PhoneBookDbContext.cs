using Microsoft.EntityFrameworkCore;
using PhoneBook.Model;

namespace PhoneBook;

internal class PhoneBookDbContext : DbContext
{
    public DbSet<PhoneBookDetails> PhoneBookDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PhoneBookDb;Trusted_Connection=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PhoneBookDetails>().HasData(
            new PhoneBookDetails
            {
                Id = 1,
                Name = "John Doe",
                PhoneNumber = "9876543210",
                EmailAddress = "john.doe@example.com"
            },
            new PhoneBookDetails
            {
                Id = 2,
                Name = "Jane Smith",
                PhoneNumber = "9123456780",
                EmailAddress = "jane.smith@example.com"
            },
            new PhoneBookDetails
            {
                Id = 3,
                Name = "Michael Johnson",
                PhoneNumber = "9000012345",
                EmailAddress = "michael.j@example.com"
            }
        );
    }
}
