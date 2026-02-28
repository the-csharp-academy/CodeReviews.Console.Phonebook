using PhoneBook.m1chael888.Models;

namespace PhoneBook.m1chael888.Data
{
    public static class SeedData
    {
        public static List<Contact> GetSeedContacts() => [
            new() { Name = "Plumber Luke", Email = "lplumbing@mail.xx", PhoneNumber = "1357924680" },
            new() { Name = "Bob", Email = "", PhoneNumber = "1234567890" },
            new() { Name = "Jack D", Email = "jackd123@coldmail.moc", PhoneNumber = "0987654321" }
            ];
    }
}