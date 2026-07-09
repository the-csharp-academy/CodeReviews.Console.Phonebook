using Microsoft.Extensions.Configuration;
using PhoneBook.Menus;
using PhoneBook.Service;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var emailService = new EmailService(configuration);

UserInterface userInterface = new UserInterface(configuration);

userInterface.MainMenu();