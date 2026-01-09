using MailKit.Net.Smtp;

namespace PhoneBook.TerrenceLGee.Services.Interfaces;

public interface ISmtpClientFactory
{
    ISmtpClient Create();
}
