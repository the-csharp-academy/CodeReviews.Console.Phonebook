using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace kilozdazolik.Phonebook.Services;

public class EmailService
{
    private readonly string _smtpServer;
    private readonly int _port;
    private readonly string _senderEmail;
    private readonly string _senderPassword;
    private readonly bool _enableSsl;

    public EmailService(IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection("EmailSettings");
        _smtpServer = emailSettings["SmtpServer"];
        _port = int.Parse(emailSettings["Port"]);
        _senderEmail = emailSettings["SenderEmail"];
        _senderPassword = emailSettings["SenderPassword"];
        _enableSsl = bool.Parse(emailSettings["EnableSsl"]);
    }

    public void SendEmail(string to, string subject, string body)
    {
        using var client = new SmtpClient(_smtpServer, _port)
        {
            Credentials = new NetworkCredential(_senderEmail, _senderPassword),
            EnableSsl = _enableSsl
        };

        client.Send(_senderEmail, to, subject, body);
    }
}