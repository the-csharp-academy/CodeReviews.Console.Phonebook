using System.Globalization;
using System.Text.RegularExpressions;

namespace PhoneBook.TerrenceLGee.Validation;

public static class EmailValidation
{
    public static bool IsValidEmailAddress(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        try
        {
            email = Regex
                .Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(1000));

            string DomainMapper(Match match)
            {
                var idn = new IdnMapping();

                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            var regularExpression = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex
                .IsMatch(email, regularExpression, RegexOptions.IgnoreCase, TimeSpan.FromMicroseconds(1000));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}
