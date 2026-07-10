using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;

namespace PhoneBook.Tests;

public class MigrationTests
{
    [Fact]
    public void GetMigrations_ContainsUniqueCategoryNameMigration()
    {
        using var context = new PhoneBookContext();

        Assert.Contains(
            "20260710000000_AddUniqueCategoryName",
            context.Database.GetMigrations());
    }
}
