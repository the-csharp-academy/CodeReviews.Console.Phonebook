using PhoneBook.Configuration;
using PhoneBook.Helpers;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PhoneBook.Services;

public class SmsService
{
    public OperationResult SendSms(TwilioSettings settings, string toPhoneNumber, string message)
    {
        if (settings is null || !settings.IsConfigured)
        {
            return OperationResult.Failed("Twilio settings are incomplete.");
        }

        if (!Validation.IsValidE164PhoneNumber(settings.FromPhoneNumber) ||
            !Validation.IsValidE164PhoneNumber(toPhoneNumber))
        {
            return OperationResult.Failed("Twilio requires sender and recipient phone numbers in E.164 format.");
        }

        if (!Validation.IsValidSmsMessage(message))
        {
            return OperationResult.Failed("SMS messages must contain 1 to 1600 characters.");
        }

        try
        {
            TwilioClient.Init(settings.AccountSid, settings.AuthToken);
            var sentMessage = MessageResource.Create(
                body: message,
                from: new PhoneNumber(settings.FromPhoneNumber),
                to: new PhoneNumber(toPhoneNumber));

            return OperationResult.Succeeded($"SMS sent ({sentMessage.Sid}).");
        }
        catch (Exception exception)
        {
            return OperationResult.Failed(exception.Message);
        }
    }
}
