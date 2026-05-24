using Microsoft.EntityFrameworkCore;
using Phonebook.EntityFramework.Domain.Entities;

namespace Phonebook.EntityFramework;

internal class ContactContext: DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite($"Data Source = contacts.db");
}
