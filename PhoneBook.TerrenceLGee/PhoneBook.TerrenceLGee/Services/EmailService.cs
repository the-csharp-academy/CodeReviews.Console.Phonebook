using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using PhoneBook.TerrenceLGee.SendEmailModels;
using PhoneBook.TerrenceLGee.Services.Interfaces;

namespace PhoneBook.TerrenceLGee.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;
    private readonly ISmtpClientFactory _smtpClientFactory;
    private string _errorMessage = string.Empty;

    public EmailService(
        IConfiguration configuration,
        ILogger<EmailService> logger,
        ISmtpClientFactory smtpClientFactory)
    {
        _configuration = configuration;
        _logger = logger;
        _smtpClientFactory = smtpClientFactory;
    }

    public async Task<bool> SendEmailAsync(EmailData emailData)
    {
        try
        {
            var senderEmail = _configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            var smtpPassword = _configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            var smtpHost = _configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            var portString = _configuration.GetValue<string>("EMAIL_CONFIGURATION:PORT");

            int port = 0;

            if (!int.TryParse(portString, out port)) return false;

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(emailData.SenderName, senderEmail));
            email.To.Add(new MailboxAddress(emailData.RecieverName, emailData.RecieverEmail));

            email.Subject = emailData.Subject;

            var builder = new BodyBuilder();

            builder.TextBody = emailData.Body;
            email.Body = builder.ToMessageBody();

            using (var smtp = _smtpClientFactory.Create())
            {
                await smtp.ConnectAsync(smtpHost, port, false);

                await smtp.AuthenticateAsync(senderEmail, smtpPassword);

                await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);

                return true;
            }
        }
        catch (Exception ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(EmailService), nameof(SendEmailAsync)) +
                $"There was an unexpected error sending the email to address: {emailData.RecieverEmail}\n" +
                $"{ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return false;
        }
    }

    private string GenerateErrorMessage(string className, string methodName)
    {
        return $"\nClass: {className}\n" +
            $"Method: {methodName}\n";
    }
}
