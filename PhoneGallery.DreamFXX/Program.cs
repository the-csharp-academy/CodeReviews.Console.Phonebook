using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneGallery.DreamFXX.Data;
using PhoneGallery.DreamFXX.Services;

class Program
{
    static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PhoneGalleryContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnectionString");

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Chyba: ConnectionString není nastaven!");
            return;
        }

        optionsBuilder.UseSqlServer(connectionString);

        using var context = new PhoneGalleryContext(optionsBuilder.Options);
        Console.WriteLine("Connecting to the database...");
        context.Database.Migrate();
        Console.WriteLine("Database was created, congratulation.");
    }
}

