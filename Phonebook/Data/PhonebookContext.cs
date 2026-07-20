using Phonebook.Models;
using Microsoft.EntityFrameworkCore;

namespace Phonebook.Data
{
    public class PhonebookContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=PhonebookDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
