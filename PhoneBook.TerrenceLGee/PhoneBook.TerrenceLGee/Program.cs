using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.TerrenceLGee.Data;
using PhoneBook.TerrenceLGee.Data.Rrepositories;
using PhoneBook.TerrenceLGee.Data.Rrepositories.Interfaces;
using PhoneBook.TerrenceLGee.Extensions;
using PhoneBook.TerrenceLGee.PhonebookAppUi;
using PhoneBook.TerrenceLGee.PhonebookAppUi.Interfaces;
using PhoneBook.TerrenceLGee.Services;
using PhoneBook.TerrenceLGee.Services.Interfaces;
using Serilog;

try
{
    LoggingSetup();
    await Startup();
}
catch (Exception ex)
{
    Console.WriteLine($"An unexpected error occurred starting the program: {ex.Message}");
}

async Task Startup()
{
    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
        .AddUserSecrets("")
        .Build();

    var services = new ServiceCollection()
        .AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true))
        .AddDbContextFactory<PhonebookContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("PhonebookDb") ?? throw new
        InvalidOperationException("Connection string 'PhonebookDb' not found.")))
        .AddScoped<IPhonebookRepository, PhonebookRepository>()
        .AddScoped<IPhonebookService, PhonebookService>()
        .AddScoped<IPhonebookUi, PhonebookUi>()
        .AddScoped<ISmtpClientFactory, SmtpClientFactory>()
        .AddScoped<IEmailService, EmailService>()
        .AddSingleton(configuration);

    var serviceProvider = services.BuildServiceProvider();

    using (var context = serviceProvider.GetRequiredService<PhonebookContext>())
    {
        await context.Database.EnsureCreatedAsync();

        DatabaseInitializer.Initialize(context);

        var phonebookUi = serviceProvider.GetRequiredService<IPhonebookUi>();

        var app = new PhonebookApp(phonebookUi);

        await app.Run();
    }
}




void LoggingSetup()
{
    var loggingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
    Directory.CreateDirectory(loggingDirectory);
    var filePath = Path.Combine(loggingDirectory, "app-.txt");
    var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.File(
        path: filePath,
        rollingInterval: RollingInterval.Day,
        outputTemplate: outputTemplate)
        .CreateLogger();
}