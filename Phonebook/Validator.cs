using System;
using System.Text.RegularExpressions;

namespace Phonebook
{
    public static class Validator
    {
        public static bool IsValidNumber(string? phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            string phoneRegex = "^\\+?[1-9]\\d{1,14}$";

            return Regex.IsMatch(phoneNumber, phoneRegex);
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, emailRegex);
        }

        public static bool IsValidChoice(string? strInput, int num)
        {
            if (String.IsNullOrEmpty(strInput))
                return false;

            if (!int.TryParse(strInput, out int choice))
            {
                return false;
            }
            return 0 <= choice && choice <= num;
        }

        public static bool IsValidString(string? stringInput)
        {
            if (String.IsNullOrEmpty(stringInput))
                return false;

            foreach (char c in stringInput)
            {
                if (!(Char.IsLetter(c) || Char.IsDigit(c)) && c != '/' && c != ' ')
                    return false;
            }

            return true;
        }
    }
}
