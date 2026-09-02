using Microsoft.EntityFrameworkCore;
using Phone_Book.Models;
using PhoneBook.Models;

namespace PhoneBook
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=PhoneBook; Integrated Security=True; TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasOne(contact => contact.Category).WithMany(category => category.Contacts).HasForeignKey(contact => contact.CategoryId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>().HasData(
                new Contact { CategoryId = 2, Id = 1, PhoneNumber = "934568035", Email = "johnjohnson@gmail.com", ContactName = "John" },
                new Contact { CategoryId = 1, Id = 2, PhoneNumber = "324532653", Email = "jankowalski@yahoo.com", ContactName = "Jan" },
                new Contact { CategoryId = 1, Id = 3, PhoneNumber = "934568035", Email = "karen609@outlook.com", ContactName = "Karen" });
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Friends" },
                new Category { Id = 2, Name = "Family" });
        }
    }
}