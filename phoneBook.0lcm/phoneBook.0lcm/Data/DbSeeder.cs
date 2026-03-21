using phoneBook._0lcm.Models;

namespace phoneBook._0lcm.Data;

public class DbSeeder
{
    public static async Task SeedContactsAsync(AppDbContext db)
    {
        var sampleContacts = new List<ContactClass>
        {
            new() { Name = "John Doe", PhoneNumber = "+1 (312) 555-0175", Email = "John-Doe@gmail.com" },
            new() { Name = "Jane Doe", PhoneNumber = "+44 121 496 0329", Email = "Jane_Doe@hotmail.com" }
        };

        await db.ContactClasses.AddRangeAsync(sampleContacts);
        await db.SaveChangesAsync();
    }
}