using PhoneBook.m1chael888.Data;
using static PhoneBook.m1chael888.Data.SeedData;

namespace PhoneBook.m1chael888.Infrastructure
{
    public interface IDbSeeder
    {
        bool ShouldSeed<T>() where T : class;
        void Seed();
    }
    public class DbSeeder : IDbSeeder
    {
        public bool ShouldSeed<T>() where T : class
        {
            using var context = new PhoneBookContext();

            var set = context.Set<T>();
            return !set.Any();
        }

        public void Seed()
        {
            using var context = new PhoneBookContext();

            context.AddRange(GetSeedContacts());
            context.SaveChanges();
        }
    }
}
