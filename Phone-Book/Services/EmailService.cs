using MimeKit;
using MimeKit.Text;
using Spectre.Console;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Phone_Book.Services;

public class EmailService
{
    public static void SendEmail()
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress("Sender Name", "test@gmail.com"));

        var emailRecipient = ContactService.GetContactByEmail();
        email.To.Add(MailboxAddress.Parse(emailRecipient));

        var emailSubject = AnsiConsole.Ask<string>("Enter the subject of the email:");
        if (string.IsNullOrWhiteSpace(emailSubject))
        {
            AnsiConsole.MarkupLine("[red]Subject cannot be empty![/]");
            return;
        }

        var emailBody = AnsiConsole.Ask<string>("Enter the body of the email:");
        if (string.IsNullOrWhiteSpace(emailBody))
        {
            AnsiConsole.MarkupLine("[red]Body cannot be empty![/]");
            return;
        }

        email.Subject = emailSubject;
        email.Body = new TextPart(TextFormat.Html) { Text = emailBody };

        using (var smtp = new SmtpClient())
        {
            smtp.Connect("smtp.gmail.com", 587);

            var smtpUsername = AnsiConsole.Ask<string>("Username: ");
            var smtpPassword = AnsiConsole.Ask<string>("Password: ");
            smtp.Authenticate(smtpUsername, smtpPassword);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}