using PhoneBook.Configuration;
using PhoneBook.Services;

namespace PhoneBook.Tests;

public class CommunicationServiceTests
{
    [Fact]
    public void SendEmail_WithIncompleteSettings_ReturnsFailureWithoutSending()
    {
        var service = new EmailService();

        var result = service.SendEmail(
            new SmtpSettings(),
            "recipient@example.com",
            "Recipient",
            "Hello",
            "Message");

        Assert.False(result.Success);
        Assert.Equal("SMTP settings are incomplete.", result.Message);
    }

    [Fact]
    public void SendSms_WithIncompleteSettings_ReturnsFailureWithoutSending()
    {
        var service = new SmsService();

        var result = service.SendSms(new TwilioSettings(), "+15550103", "Message");

        Assert.False(result.Success);
        Assert.Equal("Twilio settings are incomplete.", result.Message);
    }
}
