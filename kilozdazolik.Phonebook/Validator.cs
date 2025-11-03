using System.Globalization;
using System.Text.RegularExpressions;
using PhoneNumbers;

namespace kilozdazolik.Phonebook;

public class Validator
{
    internal static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();
    
        try 
        {
            // Parse with null region - forces international format with country code
            var number = phoneUtil.Parse(phoneNumber, null);
            return phoneUtil.IsValidNumber(number);
        }
        catch (NumberParseException)
        {
            if (!phoneNumber.StartsWith("+"))
            {
                return IsValidPhoneNumber("+" + phoneNumber);
            }
            return false;
        }
    }
}