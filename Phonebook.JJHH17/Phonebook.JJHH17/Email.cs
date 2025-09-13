using System;
using System.Net.Mail;
using System.Configuration;

namespace Phonebook.JJHH17
{
    public class Email
    {
        // Email credentials pulled from App.Config file
        readonly static string emailAddress = ConfigurationManager.AppSettings["SenderAddress"];
        readonly static string emailPassword = ConfigurationManager.AppSettings["SenderPassword"];
        // SMTP server pulled from App.Config file - Please customize this to your email provider
        readonly static string smtpServer = ConfigurationManager.AppSettings["SmtpHost"];

        public static void SendEmail(string recipient, string subject, string mailBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(smtpServer); // pulled from app.config

                mail.From = new MailAddress(emailAddress); // pulled from app.config
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.Body = mailBody;

                smtp.Port = 587;
                // Pull these from app.config
                smtp.Credentials = new System.Net.NetworkCredential(emailAddress, emailPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Console.WriteLine("Email sent successfully to " + recipient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }
    }
}
