using kilozdazolik.Phonebook;
using kilozdazolik.Phonebook.Controllers;
using kilozdazolik.Phonebook.Services;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var emailService = new EmailService(configuration);

EmailController.Initialize(emailService);
UserInterface.MainMenu();