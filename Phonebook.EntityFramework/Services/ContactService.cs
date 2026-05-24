using Microsoft.Extensions.Configuration;
using Phonebook.EntityFramework.Controllers;
using Phonebook.EntityFramework.Domain.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;
using Spectre.Console;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Phonebook.EntityFramework.Services;

internal class ContactService
{
    private readonly IConfiguration _configuration;

    public ContactService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    static internal async Task SendEmailAsync()
    {
        
        var to = UserInterface.GetContactOptionInput().Email;
        var subject = AnsiConsole.Ask<string>("Write the subject: ");
        var body = AnsiConsole.Ask<string>("Write the body: ");        
        Console.WriteLine("\n=== SENDING EMAIL ===");
        Console.WriteLine($"To: {body}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");
        Console.WriteLine("=====================\n");
        Console.WriteLine("Press enter to continue:");
        Console.ReadLine();
        Console.Clear();

        var apiKey = "apikey";

        var fromEmail = "email";
        var fromName = "name";

        var client = new SendGridClient(apiKey);

        var from = new EmailAddress(fromEmail, fromName);
        var toEmail = new EmailAddress(to);

        var msg = MailHelper.CreateSingleEmail(
            from,
            toEmail,
            subject,
            plainTextContent: body,
            htmlContent: body
        );

        var response = await client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Body.ReadAsStringAsync();
            throw new Exception($"SendGrid failed: {error}");
        }
    }

    static internal void GetContacts()
    {
        var products = ContactController.GetContacts();
        UserInterface.ShowContactTable(products);

    }
    static internal void GetContact()
    {
        var contact = UserInterface.GetContactOptionInput();
        UserInterface.ShowContact(contact);
    }
    static internal void InsertContact()
    {
        var name = AnsiConsole.Ask<string>("What is the contact's name?");
        var email = AskValidEmail();
        var phoneNumber = AskValidPhone();
        var category = UserInterface.GetCategoryOptionInput();
        ContactController.AddContact(name, email, phoneNumber,category.Id);
    }
    internal static void UpdateContact()
    {
        var contact = UserInterface.GetContactOptionInput();
        var name = AnsiConsole.Ask<string>("What is the contact's new name?");
        var email = AskValidEmail();
        var phoneNumber = AskValidPhone();
        contact.Name = name;
        contact.Email = email;
        contact.PhoneNumber = phoneNumber;
        ContactController.UpdateContact(contact);
    }        
    static internal void DeleteContact()
    {
        var contact = UserInterface.GetContactOptionInput();
        ContactController.DeleteContact(contact);
    }
       
    private static string AskValidPhone()
    {
        string phone;

        do
        {
            phone = AnsiConsole.Ask<string>("Enter phone number:");

            if (!IsValidPhone(phone))
            {
                AnsiConsole.MarkupLine("[red]Invalid phone number.[/]");
            }

        } while (!IsValidPhone(phone));

        return phone;
    }
    public static bool IsValidPhone(string phone)
    {
        return Regex.IsMatch(phone, @"^\+?[0-9\s\-()]{7,20}$");
    }
    private static string AskValidEmail()
    {
        string email;

        do
        {
            email = AnsiConsole.Ask<string>("Enter email:");

            if (!IsValidEmail(email))
            {
                AnsiConsole.MarkupLine("[red]Invalid email address.[/]");
            }

        } while (!IsValidEmail(email));

        return email;
    }
    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
