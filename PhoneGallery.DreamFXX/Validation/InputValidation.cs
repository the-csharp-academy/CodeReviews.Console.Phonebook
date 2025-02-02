using System.Text.RegularExpressions;

namespace PhoneGallery.DreamFXX.Validation;

public class InputValidation
{
    public static bool IsNameValid(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return true;
    }

    public static bool IsPhoneNumberValid(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber)) return false;

        string phoneNumberRegex = @"";
        return Regex.IsMatch(phoneNumber, @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$");
    }
}

