using Microsoft.Extensions.Configuration;
using PhoneBook.Controller;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Service;

internal class EmailService
{
    private readonly EmailController emailController;

    public EmailService(IConfiguration configuration)
    {
        emailController = new EmailController(configuration);
    }

    public bool IsConfigured() 
    {
        return emailController.IsConfigured();
    }

    public void SendWelcomeEmail(string subject, Contact contact)
    {
        string body = File.ReadAllText(
            Path.Combine(AppContext.BaseDirectory, "Templates", "WelcomeEmail.html"));

        body = body.Replace("{{Name}}", contact.Name)
            .Replace("{{Phone}}", contact.PhoneNumber)
            .Replace("{{Email}}", contact.Email);

        emailController.SendEmail(subject, contact.Email, body);
    }

    public void SendEmail()
    {
        if (!emailController.IsConfigured())
        {
            AnsiConsole.MarkupLine(
            "[#ACD8AA]Email service is not configured. Emails cannot be sent[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        }


        Contact contact = GetContactOptionInput();

        if (contact == null) 
        {
            AnsiConsole.MarkupLine("[red]There are no contacts available.[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        };

        string body = File.ReadAllText(
            Path.Combine(AppContext.BaseDirectory, "Templates", "SendEmailTemplate.html"));

        string subject = AnsiConsole.Ask<string>("Subject:");

        string message = AnsiConsole.Ask<string>("Message:");

        body = body.Replace("{{Name}}", contact.Name)
           .Replace("{{Message}}", message);

        emailController.SendEmail(subject,contact.Email,body);

        AnsiConsole.MarkupLine("[green]Email sent successfully.[/]");

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();
    }

    private static Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetAllContacts();

        if (contacts.Count == 0)
        {
            
            return null;
        }

        var options = AnsiConsole.Prompt(
            new SelectionPrompt<Contact>()
            .Title("Select a Contact")
            .UseConverter(c => $"{c.Name}: [#A57982]{c.Email}[/]")
            .AddChoices(contacts));

        int id = contacts.Single(c => c.ContactId == options.ContactId).ContactId;
        Contact contact = ContactController.GetContactById(id);

        return contact;
    }
}
