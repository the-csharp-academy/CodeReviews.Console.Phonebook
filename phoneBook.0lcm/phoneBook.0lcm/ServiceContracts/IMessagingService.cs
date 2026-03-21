using phoneBook._0lcm.Models;

namespace phoneBook._0lcm.ServiceContracts;

public interface IMessagingService
{
    public Task SendEmail(ContactClass contact, string bodyText, string subjectText);
    public SmsResponseClass SendSms(ContactClass contact, string bodyText);
}