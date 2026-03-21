using System.Net.Mail;
using phoneBook._0lcm.ServiceContracts;
using PhoneNumbers;

namespace phoneBook._0lcm.Services;

public class VerificationService : IVerificationService
{
    public bool ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        try
        {
            var address = new MailAddress(email);
            return address.Address == email;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public bool ValidatePhoneNumber(string phoneNumber, string regionCode)
    {
        try
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();
            var parsedNumber = phoneUtil.Parse(phoneNumber, regionCode);
            return phoneUtil.IsValidNumber(parsedNumber);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
}