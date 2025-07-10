using Spectre.Console;
using System.Text.RegularExpressions;

namespace Phonebook
{
    internal static class ValidationHelper
    {
        internal static string ValidatePhone(string phone)
        {
            bool isPhoneValid = false;

            while (!isPhoneValid)
            {
                isPhoneValid = true;

                if (string.IsNullOrEmpty(phone))
                {
                    isPhoneValid = false;
                    AnsiConsole.WriteLine("The phone number cannot be blank.");
                    phone = AnsiConsole.Prompt(new TextPrompt<string>("Phone (xxx-xxx-xxxx): "));
                }
                else if (!Regex.IsMatch(phone, "[0-9]{3}-[0-9]{3}-[0-9]{4}"))
                {
                    isPhoneValid = false;
                    AnsiConsole.WriteLine("The phone number must be in the format of xxx-xxx-xxxx. Include the area code and the dashes.");
                    phone = AnsiConsole.Prompt(new TextPrompt<string>("Phone (xxx-xxx-xxxx): "));
                }
            }

            return phone;
        }

        internal static string ValidateEmail(string email)
        {
            bool isEmailValid = false;

            while (!isEmailValid)
            {
                isEmailValid = true;

                if (string.IsNullOrEmpty(email))
                {
                    return string.Empty;
                }
                else if (!Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
                {
                    isEmailValid = false;
                    AnsiConsole.WriteLine("The email must be in the format of a@b.cc, i.e i@m.ca or sample@gmail.com.");
                    email = AnsiConsole.Prompt(new TextPrompt<string>("Email (a@b.cc): "));
                }
            }

            return email;
        }
    }
}
