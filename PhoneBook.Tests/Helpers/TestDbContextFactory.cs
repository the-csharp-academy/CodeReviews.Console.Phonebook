using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;

namespace PhoneBook.Tests.Helpers;

public class TestPhoneBookContext : PhoneBookContext
{
    private readonly string _databaseName;

    public TestPhoneBookContext(string databaseName)
    {
        _databaseName = databaseName;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Override the base SQLite configuration with InMemory
        optionsBuilder.UseInMemoryDatabase(_databaseName);
    }
}

public static class TestDbContextFactory
{
    public static TestPhoneBookContext CreateContext()
    {
        var dbName = Guid.NewGuid().ToString();
        var context = new TestPhoneBookContext(dbName);
        context.Database.EnsureCreated();
        return context;
    }

    public static Func<PhoneBookContext> CreateContextFactory()
    {
        var databaseName = Guid.NewGuid().ToString();
        using var context = new TestPhoneBookContext(databaseName);
        context.Database.EnsureCreated();

        return () => new TestPhoneBookContext(databaseName);
    }
}
