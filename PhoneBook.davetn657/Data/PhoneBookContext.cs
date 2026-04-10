using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.davetn657.Data;

public class PhoneBookContext : DbContext
{
    private IConfiguration configuration;

    public DbSet<PhoneProperties> PhoneProperties { get; set; }
    public string? DbPath { get; set; }

    public PhoneBookContext()
    {
        configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        DbPath = configuration["Database"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}

public class PhoneProperties
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Category {  get; set; } = string.Empty;
}