using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using PhoneBook.Configuration;
using PhoneBook.Helpers;

namespace PhoneBook.Services;

public class EmailService
{
    public OperationResult SendEmail(
        SmtpSettings settings,
        string toAddress,
        string toName,
        string subject,
        string body)
    {
        if (settings is null || !settings.IsConfigured)
        {
            return OperationResult.Failed("SMTP settings are incomplete.");
        }

        if (!Validation.IsValidEmail(toAddress) || !Validation.IsValidEmail(settings.SenderEmail))
        {
            return OperationResult.Failed("A valid sender and recipient email address are required.");
        }

        if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
        {
            return OperationResult.Failed("An email subject and message are required.");
        }

        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(settings.SenderName, settings.SenderEmail));
            message.To.Add(new MailboxAddress(toName, toAddress));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Plain) { Text = body };

            using var client = new SmtpClient();
            var socketOptions = settings.UseStartTls
                ? SecureSocketOptions.StartTlsWhenAvailable
                : SecureSocketOptions.Auto;

            client.Connect(settings.Host, settings.Port, socketOptions);
            client.Authenticate(settings.Username, settings.Password);
            client.Send(message);
            client.Disconnect(true);

            return OperationResult.Succeeded("Email sent.");
        }
        catch (Exception exception)
        {
            return OperationResult.Failed(exception.Message);
        }
    }
}
