
using System.Text.RegularExpressions;

namespace PhoneBook.EntityFramework
{
    internal class Validation
    {
        internal static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        internal static bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^\+?[\d\s\-]{7,15}$");
        }
    }
}