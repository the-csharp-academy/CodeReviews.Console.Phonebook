using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Utilities.GoldRino456;
public static class ValidationUtils
{
    public static bool ValidateEmailAddress(string email)
    {
        try
        {
            new MailAddress(email);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public static bool ValidatePhoneNumber(string phone)
    {
        return Regex.IsMatch(phone, "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$");
    }
}
