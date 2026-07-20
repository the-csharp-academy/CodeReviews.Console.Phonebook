using System;
using System.Net;
using System.Net.Mail;

namespace Phonebook
{
    public class SmtpConfig
    {
        public string Host { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public string SenderEmail { get; set; } = "";
        public string Password { get; set; } = "";
        public bool EnableSsl { get; set; } = true;
    }

    public class EmailService
    {
        public bool SendEmail(string recipientEmail, string subject, string body, SmtpConfig config)
        {
            if (string.IsNullOrWhiteSpace(recipientEmail))
            {
                Console.WriteLine("\n[Email Error] Recipient email address is missing or invalid.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(config.Host) || string.IsNullOrWhiteSpace(config.SenderEmail))
            {
                Console.WriteLine("\n[Email Error] SMTP configuration is incomplete. Host and sender email are required.");
                return false;
            }

            try
            {
                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(config.SenderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(recipientEmail);

                using var smtpClient = new SmtpClient(config.Host, config.Port)
                {
                    Credentials = new NetworkCredential(config.SenderEmail, config.Password),
                    EnableSsl = config.EnableSsl
                };

                smtpClient.Send(mailMessage);
                Console.WriteLine($"\nEmail successfully sent to {recipientEmail}!");
                return true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"\n[SMTP Error] Failed to send email: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Details: {ex.InnerException.Message}");
                }
                return false;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"\n[Email Error] Invalid email format: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Unexpected Email Error] {ex.Message}");
                return false;
            }
        }
    }
}
