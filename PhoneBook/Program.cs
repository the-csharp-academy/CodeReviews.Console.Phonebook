using DotNetEnv;
using PhoneBook.UI;

Env.Load();
ConsoleMenu menu = new();
await menu.OnStart();
