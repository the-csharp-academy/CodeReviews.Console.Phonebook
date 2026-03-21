using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using phoneBook._0lcm.Configuration;
using phoneBook._0lcm.Data;
using phoneBook._0lcm.Logging;
using phoneBook._0lcm.ServiceContracts;
using phoneBook._0lcm.Services;
using phoneBook._0lcm.UserInterface;

var builder = Host.CreateApplicationBuilder(args);
var settingsManager = new SettingsManager();
var appSettings = settingsManager.Load();

var dbPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "phoneBook.0lcm",
    "app.db");

builder.Services.AddSingleton(appSettings);
builder.Services.AddSingleton(settingsManager);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddTransient<IVerificationService, VerificationService>();

builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<ContactUi>();

builder.Services.AddTransient<IMessagingService, MessagingService>();
builder.Services.AddTransient<MessagingUi>();

builder.Services.AddTransient<ISettingService, SettingService>();
builder.Services.AddTransient<SettingUi>();

builder.Services.AddSingleton<ConsoleUi>();

builder.Services.AddHostedService<Worker>();

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Warning);
builder.Logging.AddConsole(options => { options.FormatterName = "customFormatter"; });
builder.Logging.AddConsoleFormatter<CustomFormatter, ConsoleFormatterOptions>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var dbDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "phoneBook.0lcm");
    Directory.CreateDirectory(dbDirectory);

    var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
    var isFirstRun = pendingMigrations.Any();

    await db.Database.MigrateAsync();

    if (isFirstRun) await DbSeeder.SeedContactsAsync(db);
}

await app.RunAsync();

internal class Worker : BackgroundService
{
    private readonly ConsoleUi _console;

    public Worker(ConsoleUi consoleUi)
    {
        _console = consoleUi;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _console.MainMenu();
    }
}