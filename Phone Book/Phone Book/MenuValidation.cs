using System.Net.Mail;

namespace PhoneBook
{
    internal static class MenuValidation
    {
        public static bool EmailValidation(string email)
        {
            MailAddress emailAddress;
            bool correctEmailBool = MailAddress.TryCreate(email, out emailAddress);
            if (correctEmailBool)
            {
                if (emailAddress.Host.Contains("."))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static bool PhoneValidation(string phoneNumber)
        {
            bool isPhoneNumberEmpty = string.IsNullOrWhiteSpace(phoneNumber);
            if (!isPhoneNumberEmpty)
            {
                if (phoneNumber.StartsWith("+"))
                {
                    if (phoneNumber.Length > 1)
                    {
                        bool isNumericPlus = phoneNumber.Skip(1).All(char.IsDigit);
                        return isNumericPlus;
                    }
                }
                bool isNumeric = phoneNumber.All(char.IsDigit);
                return isNumeric;
            }
            return false;
        }
    }
}