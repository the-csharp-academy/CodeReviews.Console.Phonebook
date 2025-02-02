using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PhoneGallery.DreamFXX.Data;

public class PhoneGalleryContextFactory : IDesignTimeDbContextFactory<PhoneGalleryContext>
{
    public PhoneGalleryContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PhoneGalleryContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));

        return new PhoneGalleryContext(optionsBuilder.Options);
    }
}
