using MailKit.Net.Smtp;
using PhoneBook.TerrenceLGee.Services.Interfaces;

namespace PhoneBook.TerrenceLGee.Services;

public class SmtpClientFactory : ISmtpClientFactory
{
    public ISmtpClient Create()
    {
        return new SmtpClient();
    }
}
