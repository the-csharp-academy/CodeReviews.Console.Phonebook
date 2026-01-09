using PhoneBook.TerrenceLGee.SendEmailModels;

namespace PhoneBook.TerrenceLGee.Services.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailData emailData);
}
