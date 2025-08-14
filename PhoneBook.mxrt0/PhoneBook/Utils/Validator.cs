using PhoneBook.Utils.Enums;
using System.Text.RegularExpressions;

namespace PhoneBook.Utils;

public static class Validator
{
    public static bool IsUserInputValid(string? input)
    {
        return !string.IsNullOrEmpty(input) && Enum.TryParse(typeof(MenuOption), input, out var option)
        && Enum.IsDefined(typeof(MenuOption), option);
    }

    public static bool IsContactNameInputValid(string? input)
    {
        return !string.IsNullOrEmpty(input) && input.Any(char.IsLetter);
    }

    public static bool IsContactEmailInputValid(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        input = input.Trim();

        string pattern = @"^[A-Za-z0-9._]+@[A-Za-z0-9.-]+\.[a-z]{2,}$";

        return Regex.IsMatch(input, pattern);
    }

    public static bool IsContactPhoneInputValid(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;

        input = input.Trim();

        if (input.Length > 20)
        {
            return false;
        }

        if (input[0] == '+')
        {
            input = input.Substring(1);
        }

        if (!input.Any(char.IsDigit))
        {
            return false;
        }

        if (!char.IsDigit(input[0]) || !char.IsDigit(input[^1]))
        {
            return false;
        }

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsDigit(c)) continue;
            if (c == '-')
            {
                if (input[i - 1] == '-')
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}

