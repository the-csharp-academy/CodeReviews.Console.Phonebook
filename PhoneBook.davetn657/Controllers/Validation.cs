using System.Net.Mail;
using System.Text.RegularExpressions;

namespace PhoneBook.davetn657.Controllers;
public class Validation
{

    public bool ValidatePhoneNumber(string number)
    {
        var phonePattern = @"^\d{3}-\d{3}-\d{4}$";

        return !string.IsNullOrEmpty(number) && Regex.IsMatch(number, phonePattern);
    }

    public bool ValidateEmail(string email)
    {
        if(string.IsNullOrEmpty(email)) return false;
        try
        {
            var address = new MailAddress(email);

            return MailAddress.TryCreate(email, out address);
        }
        catch(FormatException)
        {
            return false;
        }
    }
}