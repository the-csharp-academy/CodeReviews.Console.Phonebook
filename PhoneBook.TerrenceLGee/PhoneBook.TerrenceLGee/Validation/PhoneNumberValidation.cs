using PhoneBook.TerrenceLGee.Entities;
using PhoneNumbers;

namespace PhoneBook.TerrenceLGee.Validation;

public static class PhoneNumberValidation
{
    public static bool IsValidPhoneNumber(ContactPhoneNumber number)
    {
        if (string.IsNullOrEmpty(number.Number)) return false;

        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        var phoneNumber = phoneNumberUtil.Parse(number.Number, number.CountryCode.ToString());

        return phoneNumberUtil.IsValidNumber(phoneNumber);
    }
}
