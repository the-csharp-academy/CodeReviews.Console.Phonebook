using Microsoft.EntityFrameworkCore;
using PhoneBook.m1chael888.Models;
using Microsoft.Extensions.Configuration;

namespace PhoneBook.m1chael888.Data
{
    internal class PhoneBookContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .Build();
            string connectionString = builder.GetConnectionString("ConnectionString");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}