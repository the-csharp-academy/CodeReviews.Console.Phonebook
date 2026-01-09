using PhoneBook.TerrenceLGee.DTOs.ContactDTOs;
using PhoneBook.TerrenceLGee.DTOs.ContactPhoneNumberDTOs;
using PhoneBook.TerrenceLGee.Entities.Enums;
using PhoneBook.TerrenceLGee.Mappings.ContactPhoneNumberMappings;
using PhoneBook.TerrenceLGee.Validation;
using Spectre.Console;

namespace PhoneBook.TerrenceLGee.PhonebookAppUi.Helpers;

public static class ContactUpdateHelpers
{
    public static UpdateContactDto UpdateContact(RetrievedContactDto contact, List<UpdateProperties> properties)
    {
        string? updatedFirstName = null;
        string? updatedLastName = null;
        string? updatedEmail = null;
        List<UpdateContactPhoneNumberDto> updatedPhoneNumbers = [];
        ContactCategory? updatedCategory = null;
        string? updatedNotes = null;
        string? updatedSuffix = null;
        DateOnly? updatedBirthDate = null;
        string? updatedNickname = null;

        for (int i = 0; i < properties.Count; i++)
        {
            switch (properties[i])
            {
                case UpdateProperties.FirstName:
                    updatedFirstName = InputHelpers.GetInput("Enter updated first name ");
                    break;
                case UpdateProperties.LastName:
                    updatedLastName = InputHelpers.GetInput("Enter updated last name ");
                    break;
                case UpdateProperties.Email:
                    updatedEmail = InputHelpers.GetValidEmailAddress();
                    break;
                case UpdateProperties.PhoneNumbers:
                    updatedPhoneNumbers = UpdatePhoneNumbers(contact.PhoneNumbers.ToList(), contact.Id);
                    break;
                case UpdateProperties.Category:
                    updatedCategory = InputHelpers.GetContactCategory();
                    break;
                case UpdateProperties.Notes:
                    updatedNotes = InputHelpers.GetInput("Enter updated notes ");
                    break;
                case UpdateProperties.Suffix:
                    updatedSuffix = InputHelpers.GetInput("Enter updated suffix ");
                    break;
                case UpdateProperties.DateOfBirth:
                    updatedBirthDate = InputHelpers.GetBirthDate();
                    break;
                case UpdateProperties.Nickname:
                    updatedNickname = InputHelpers.GetInput("Enter updated nickname ");
                    break;
            }
        }

        return new UpdateContactDto
        {
            Id = contact.Id,
            FirstName = updatedFirstName ?? contact.FirstName,
            LastName = updatedLastName ?? contact.LastName,
            Email = updatedEmail ?? contact.Email,
            PhoneNumbers = (updatedPhoneNumbers.Count == 0) 
            ? contact.PhoneNumbers.ToList().FromRetrievedToUpdatePhoneNumberDtoList()
            : updatedPhoneNumbers,
            Category = updatedCategory ?? contact.Category,
            Notes = updatedNotes ?? contact.Notes,
            Suffix = updatedSuffix ?? contact.Suffix,
            DateOfBirth = updatedBirthDate ?? contact.DateOfBirth,
            NickName = updatedNickname ?? contact.NickName,
        };

    }


    public static List<UpdateContactPhoneNumberDto> UpdatePhoneNumbers(
        List<RetrievedContactPhoneNumberDto> phoneNumbers, 
        int contactId)
    {
        var updatedPhoneNumbers = new List<UpdateContactPhoneNumberDto>();

        var count = phoneNumbers.Count;

        if (count == 0)
        {
            count = AnsiConsole.Ask<int>("[DarkOrange]This contact currently has no phone numbers to update\n" +
                "Please enter how many phone numbers you will be adding to this contact: [/]");
            var createdPhoneNumbers = InputHelpers.CreatePhoneNumbers(count);
            updatedPhoneNumbers = createdPhoneNumbers.FromCreateToUpdateContactPhoneNumberDtoList();
            AddIdToUpdatePhoneNumbers(updatedPhoneNumbers, contactId);
            return updatedPhoneNumbers;
        }

        for (int i = 0; i < count; i++)
        {
            var updateNumber = AnsiConsole
                .Confirm($"[DarkOliveGreen1_1]Do you wish to update: {phoneNumbers[i].Number}? [/]");
            if (updateNumber)
            {
                var updatedPhoneNumber = GetUpdatedPhoneNumber(phoneNumbers[i]);
                updatedPhoneNumbers.Add(updatedPhoneNumber);
            }
        }

        return updatedPhoneNumbers;
    }

    public static UpdateContactPhoneNumberDto GetUpdatedPhoneNumber(RetrievedContactPhoneNumberDto phoneNumber)
    {
        Console.Clear();
        var number = string.Empty;
        var isValidPhoneNumber = false;
        var updatedPhoneNumber = new UpdateContactPhoneNumberDto();
        CountryCode countryCode;
        PhoneNumberCategory category;

        while (!isValidPhoneNumber)
        {
            var updateNumber = AnsiConsole
            .Confirm($"[Cyan2]Update phone number {phoneNumber.Number}? [/]");

            number = (updateNumber) 
                ? InputHelpers.GetInput("Enter updated phone number ") 
                : phoneNumber.Number;

            var updateCountryCode = AnsiConsole
                .Confirm($"[Cyan2]Do you wish to update the country code: {phoneNumber.CountryCode}? [/]");

            countryCode = (updateCountryCode)
                ? InputHelpers.GetCountryCode()
                : phoneNumber.CountryCode;

            updatedPhoneNumber.Number = number;
            updatedPhoneNumber.CountryCode = countryCode;

            isValidPhoneNumber = PhoneNumberValidation
                .IsValidPhoneNumber(updatedPhoneNumber.FromUpdateContactPhoneNumberDto());

            if (!isValidPhoneNumber)
            {
                UiHelpers
                    .PressAnyKeyToContinueError("The phone number that you entered was invalid");
            }
        }

        var updatePhoneNumberCategory = AnsiConsole
            .Confirm($"[Cyan2]Do you wish to update the phone number's category:" +
            $" {phoneNumber.PhoneNumberCategory}? [/]");

        category = (updatePhoneNumberCategory)
            ? InputHelpers.GetPhoneNumberCategory()
            : phoneNumber.PhoneNumberCategory;

        updatedPhoneNumber.PhoneNumberCategory = category;

        return updatedPhoneNumber;
    }

    private static void AddIdToUpdatePhoneNumbers(List<UpdateContactPhoneNumberDto> phoneNumbers, int contactId)
    {
        foreach (var phoneNumber in phoneNumbers)
        {
            phoneNumber.ContactId = contactId;
        }
    }
}
