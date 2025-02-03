using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneGallery.DreamFXX.Data;
using PhoneGallery.DreamFXX.Services;

var host = CreateHostBuilder(args).Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var phoneGalleryService = services.GetRequiredService<PhoneGalleryService>();
    phoneGalleryService.Start();
}
catch (Exception exxx)
{

    Console.WriteLine($"Error occured while starting the app. Please check your configuration and try again.\ninfo: {exxx.Message}");
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, configuration) =>
    {
        configuration.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<PhoneGalleryContext>(opt =>
            opt.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnectionString")));
        services.AddTransient<PhoneGalleryService>();
    });


//var configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .Build();

//var optionsBuilder = new DbContextOptionsBuilder<PhoneGalleryContext>();
//var connectionString = configuration.GetConnectionString("DefaultConnectionString");

//if (string.IsNullOrEmpty(connectionString))
//{
//    Console.WriteLine("Chyba: ConnectionString není nastaven!");
//    return;
//}

//optionsBuilder.UseSqlServer(connectionString);

//using var context = new PhoneGalleryContext(optionsBuilder.Options);
//Console.WriteLine("Connecting to the database...");
//context.Database.Migrate();
//Console.WriteLine("Database was created.");
