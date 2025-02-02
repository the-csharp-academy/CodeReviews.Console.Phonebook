using System.Text.RegularExpressions;

namespace PhoneGallery.DreamFXX.Validation;

public class InputValidation
{
    public static bool IsNameValid(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        if (name.Length < 2 || name.Length > 100)
        {
            Console.WriteLine("The name is not in the correct format. Name can contain from 2 to 100 characters. Please try again.");
            return false;
        }

        return true;
    }

    public static bool IsPhoneNumberValid(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        string phoneNumberRegex = @"^\+?([0-9]{1,3})?[-. ]?\(?([0-9]{1,3})\)?[-. ]?([0-9]{3,4})[-. ]?([0-9]{4})$";

        if (!Regex.IsMatch(phoneNumber, phoneNumberRegex))
        {
            Console.WriteLine("The phone number is not in the correct format. Please try again.");
            return false;
        }

        return true;
    }

    public static bool IsEmailValid(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if (!Regex.IsMatch(email, emailRegex))
        {
            Console.WriteLine("The email is not in the correct format. Please try again and enter valid email address.");
            return false;
        }
        return true;
    }
}

