

using Microsoft.EntityFrameworkCore.Design;

namespace PhoneBook.GoldRino456.Data;
public class ContactContextFactory : IDesignTimeDbContextFactory<ContactContext>
{
    public ContactContext CreateDbContext(string[] args)
    {
        if(!AppConfig.FetchConnectionString(out var connectionString))
        {
            throw new InvalidOperationException("Could not find connection string in appsettings.json.");
        }

        return new ContactContext(connectionString);
    }
}
