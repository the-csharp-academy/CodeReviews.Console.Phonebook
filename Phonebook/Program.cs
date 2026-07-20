using System;
using Phonebook;
using Phonebook.Data;

try
{
    using PhonebookContext context = new();

    try
    {
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nDatabase initialization issue: {ex.Message}");
    }

    var contactService = new ContactService(context);

    contactService.AddContact("Messi", "+54911234567", "messi@goat.com", "Football");
    contactService.AddContact("Max Verstappen", "+31202234567", "max33@redbull.com", "F1");
    contactService.AddContact("Mum", "+84123456789", "abc@gmail.com", "Family");

    var userInput = new UserInput(contactService);
}
catch (Exception ex)
{
    Console.WriteLine($"\nAn unhandled error occurred: {ex.Message}");
}

