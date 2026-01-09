using Microsoft.EntityFrameworkCore;
using PhoneBook.TerrenceLGee.Entities;

namespace PhoneBook.TerrenceLGee.Data;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactPhoneNumber> PhoneNumbers { get; set; }

    public PhonebookContext(DbContextOptions<PhonebookContext> options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasMany<ContactPhoneNumber>(c => c.PhoneNumbers)
            .WithOne(p => p.Contact)
            .HasForeignKey(p => p.ContactId);

        modelBuilder.Entity<Contact>().ToTable(nameof(Contact));
        modelBuilder.Entity<ContactPhoneNumber>().ToTable(nameof(ContactPhoneNumber));

        modelBuilder.Entity<Contact>()
            .Property(c => c.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Contact>()
            .Property(c => c.LastName)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Contact>()
            .Property(c => c.Notes)
            .HasMaxLength(300);

        modelBuilder.Entity<ContactPhoneNumber>()
            .Property(p => p.Number)
            .HasMaxLength(17)
            .IsRequired();
    }
}
