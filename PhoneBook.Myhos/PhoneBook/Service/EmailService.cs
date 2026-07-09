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


        string email = GetContactEmailOptionInput();

        if (email == null) 
        {
            AnsiConsole.MarkupLine("[red]There are no contacts available.[/]");

            Console.WriteLine("Enter any key to continue...");
            Console.ReadLine();

            return;
        };

        string subject = AnsiConsole.Ask<string>("Subject:");

        string message = AnsiConsole.Ask<string>("Message:");

        emailController.SendEmail(subject,email,message);

        AnsiConsole.MarkupLine("[green]Email sent successfully.[/]");

        AnsiConsole.MarkupLine("[red]There are no contacts available.[/]");

        Console.WriteLine("Enter any key to continue...");
        Console.ReadLine();
    }

    private static string GetContactEmailOptionInput()
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

        return options.Email;
    }
}
