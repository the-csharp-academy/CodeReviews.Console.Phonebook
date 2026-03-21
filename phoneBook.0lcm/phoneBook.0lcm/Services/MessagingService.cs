using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Text.Json;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using phoneBook._0lcm.Configuration;
using phoneBook._0lcm.Models;
using phoneBook._0lcm.ServiceContracts;
using PhoneNumbers;

namespace phoneBook._0lcm.Services;

public class MessagingService(AppSettings appSettings) : IMessagingService
{
    private const string SmsApi = "http://textbelt.com/text";

    public async Task SendEmail(ContactClass contact, string bodyText, string subjectText)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(appSettings.UserName, appSettings.UserEmail!));
        message.To.Add(new MailboxAddress(contact.Name, contact.Email));

        message.Subject = subjectText;

        message.Body = new TextPart("plain")
        {
            Text = bodyText
        };

        var smptHost = GetSmptHost(appSettings.UserEmail!);

        using var client = new SmtpClient();

        await client.ConnectAsync(smptHost, 587, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(appSettings.UserEmail, appSettings.UserAppPassword);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    public SmsResponseClass SendSms(ContactClass contact, string bodyText)
    {
        string phone;
        if (contact.PhoneNumber.Contains("+1"))
        {
            phone = contact.PhoneNumber;
        }
        else
        {
            string ToE164(string input, string defaultRegion = "ZZ")
            {
                var phoneUtil = PhoneNumberUtil.GetInstance();
                var parsed = phoneUtil.Parse(input, defaultRegion);
                return phoneUtil.Format(parsed, PhoneNumberFormat.E164);
            }

            phone = ToE164(contact.PhoneNumber);
        }

        using (var client = new WebClient())
        {
            var response = client.UploadValues(SmsApi, new NameValueCollection
            {
                { "phone", phone },
                { "message", bodyText },
                { "key", appSettings.TextBeltKey }
            });

            var rawjson = Encoding.UTF8.GetString(response);

            return JsonSerializer.Deserialize<SmsResponseClass>(rawjson)!;
        }
    }

    //------- Helper Methods -------
    private string GetSmptHost(string receiverEmailAddress)
    {
        var smptHostServer = "";
        var domain = receiverEmailAddress.Split('@')[1];
        domain = domain.ToLower();

        if (domain.Contains("gmail.com"))
            smptHostServer = "smtp.gmail.com";
        else if (domain.Contains("hotmail.com") || domain.Contains("@outlook.com"))
            smptHostServer = "smtp.office365.com";
        else if (domain.Contains("yahoo.com"))
            smptHostServer = "smpt.mail.yahoo.com";
        else
            throw new ArgumentException("Email does not contain a valid domain");

        return smptHostServer;
    }
}