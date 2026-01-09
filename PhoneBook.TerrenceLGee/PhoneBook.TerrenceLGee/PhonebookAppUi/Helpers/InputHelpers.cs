using PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;
using PhoneBook.TerrenceLGee.Entities.Enums;
using PhoneBook.TerrenceLGee.Validation;
using PhoneBook.TerrenceLGee.Mappings.ContactPhoneNumberMappings;
using Spectre.Console;

namespace PhoneBook.TerrenceLGee.PhonebookAppUi.Helpers;

public static class InputHelpers
{
    private const string DateFormat = "MM-dd-yyyy";

    public static string GetInput(string phrase)
    {
        var validInput = false;
        var input = string.Empty;

        while (!validInput)
        {
            input = AnsiConsole.Ask<string>($"[DarkSeaGreen1]Enter {phrase}: [/]");
            validInput = AnsiConsole.Confirm($"[DarkTurquoise]You entered '{input}' is this correct? [/]");
        }
        return input;
    }

    public static string? GetValidEmailAddress()
    {
        var emailAddress = string.Empty;
        var isValidEmail = false;
        var tries = 3;

        while (!isValidEmail && tries > 0)
        {
            emailAddress = AnsiConsole.Ask<string>($"[DarkOliveGreen2]Enter the contact's email address: [/]");
            isValidEmail = EmailValidation.IsValidEmailAddress(emailAddress);

            if (!isValidEmail)
            {
                UiHelpers
                    .PressAnyKeyToContinueError($"The email address you entered is invalid\n" +
                    $"You have {tries--} tries left to enter a valid email address");
            }
        }

        return emailAddress;
    }

    public static List<CreateContactPhoneNumberDto> CreatePhoneNumbers(int count)
    {
        var phoneNumbers = new List<CreateContactPhoneNumberDto>();

        for (int i = 0; i < count; i++)
        {
            var phoneNumber = GetCreatedPhoneNumber();
            phoneNumbers.Add(phoneNumber);
        }

        return phoneNumbers;
    }

    

    public static CreateContactPhoneNumberDto GetCreatedPhoneNumber()
    {
        var number = string.Empty;
        var isValidPhoneNumber = false;
        var phoneNumber = new CreateContactPhoneNumberDto();
        CountryCode countryCode;
        PhoneNumberCategory category;

        while (!isValidPhoneNumber)
        {
            number = AnsiConsole.Ask<string>("[Aquamarine1]Enter the contact's phone number: [/]");
            countryCode = GetCountryCode();
            phoneNumber.Number = number;
            phoneNumber.CountryCode = countryCode;
            isValidPhoneNumber = PhoneNumberValidation.IsValidPhoneNumber(phoneNumber.FromCreateContactPhoneNumberDto());

            if (!isValidPhoneNumber)
            {
                UiHelpers.PressAnyKeyToContinueError("The phone number that you entered was invalid");
            }
        }

        category = GetPhoneNumberCategory();
        phoneNumber.PhoneNumberCategory = category;
        return phoneNumber;
    }

    public static CountryCode GetCountryCode()
    {
        var codes = Enum
            .GetValues<CountryCode>()
            .ToList()
            .OrderBy(c => c.ToString());

        return AnsiConsole.Prompt(
            new SelectionPrompt<CountryCode>()
            .Title("[DarkOliveGreen1_1]Please select the contact's country code: [/]")
            .PageSize(15)
            .AddChoices(codes));
    }

    public static PhoneNumberCategory GetPhoneNumberCategory()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<PhoneNumberCategory>()
            .Title("[DarkSeaGreen1]Please select the category for this phone number: [/]")
            .AddChoices(Enum.GetValues<PhoneNumberCategory>()));
    }

    public static DateOnly GetBirthDate()
    {
        DateOnly birthDate;

        var dateString = AnsiConsole
            .Ask<string>($"[Gold3_1]Enter contact birthdate in format {DateFormat}: [/]");

        while (!DateOnly.TryParseExact(dateString, DateFormat, out birthDate))
        {
            UiHelpers.PressAnyKeyToContinueError("Invalid birth date entered");
            dateString = AnsiConsole
            .Ask<string>($"[Gold3_1]Enter contact birthdate in format {DateFormat}: [/]");
        }

        return birthDate;
    }

    public static ContactCategory GetContactCategory()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<ContactCategory>()
            .Title("[LightCyan3]Please enter the category for this contact: [/]")
            .AddChoices(Enum.GetValues<ContactCategory>()));
    }
}
