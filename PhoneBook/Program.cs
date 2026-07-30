using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Repository;
using PhoneBook.UI;

Env.Load();
using (var context = new PhoneBookContext())
{
    context.Database.Migrate();
}
ConsoleMenu menu = new();
await menu.OnStart();
