using Microsoft.Extensions.Configuration;
using PhoneBook.Models;
using System.Net;
using System.Net.Mail;

namespace PhoneBook.Controller;

internal class EmailController
{
    private readonly MailSettings _settings;

    public EmailController(IConfiguration configuration)
    {
        _settings = configuration
            .GetSection("MailSettings")
            .Get<MailSettings>()!;
    }

    public bool IsConfigured()
    {
        return !string.IsNullOrWhiteSpace(_settings.Host)
            && _settings.Port > 0
            && !string.IsNullOrWhiteSpace(_settings.Username)
            && !string.IsNullOrWhiteSpace(_settings.Password)
            && !string.IsNullOrWhiteSpace(_settings.From);
    }

    public void SendEmail(string subject, string to, string body)
    {
        using var message = new MailMessage();

        message.From = new MailAddress(_settings.From);
        message.To.Add(to);

        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;

        using var smtp = new SmtpClient(_settings.Host, _settings.Port)
        {
            Credentials = new NetworkCredential(
                _settings.Username,
                _settings.Password),

            EnableSsl = true
        };

        smtp.Send(message);
    }
}