using PhoneBook.Utils.Enums;

namespace PhoneBook.Utils
{
    public static class Messages
    {
        public static readonly string MainMenuMessage = "\nContacts Main Menu\n\n- - - - - - - - - - - - - -";

        public static readonly string PressAnyKeyToContinueMessage = "\nPress any key to continue...\n";

        public static readonly string ReturnToMainMenuMessage = "\nType 0 to return to the Main Menu:\n";

        public static readonly string InvalidInputMessage = $"\nInvalid input. Please enter a number between 0 and {Enum.GetValues<MenuOption>().Length - 1}!\n";

        public static readonly string ExitMessage = "\nGoodbye!...\n";

        public static readonly string ContactNameMessage = "\nEnter the name of the contact:\n";

        public static readonly string InvalidContactNameMessage = "\nInvalid name. Please enter a non-empty string containing at least 1 letter!\n";

        public static readonly string ContactEmailMessage = "\nEnter the e-mail of the contact:\n";

        public static readonly string InvalidContactEmailMessage = "\nInvalid e-mail. Left part must have at least 1 letter, there should a domain section (e.g. xyz.com) and an @ separating the two parts!\n";

        public static readonly string ContactPhoneMessage = "\nEnter the phone number of the contact (max length 20):\n";

        public static readonly string ContactCategoryMesage = "\nEnter the category the contact will belong to (it must already exist):\n";

        public static readonly string InvalidContactPhoneMessage = "\nInvalid phone number. Must be at most 20 characters, start with an optional '+', contain only digits, and may include dashes '-' between digits.\n";

        public static readonly string SuccessfullyAddedContactMessage = "\nSuccessfully added new contact!\n";

        public static readonly string DeleteContactIdMessage = "\nEnter the ID of the contact you wish to delete:\n";

        public static readonly string ContactDoesNotExistMessage = "\nContact with this name/ID doesn't exist. Try again!\n";

        public static readonly string InvalidIdMessage = "\nInvalid ID. Please enter a positive integer!\n";

        public static readonly string SuccessfullyDeletedContactMessage = "\nSuccessfully deleted contact with ID {0}!\n";

        public static readonly string UpdateContactIdMessage = "\nEnter the ID of the contact you wish to update:\n";

        public static readonly string SuccessfullyUpdatedContactMessage = "\nSuccessfully updated contact with ID {0}!\n";

        public static readonly string CategoryNameDoesNotExistMessage = "\nNo category with this name exists. Try again!\n";

        public static readonly string CategoryNameMessage = "\nEnter the name of the category:\n";

        public static readonly string CategoryAlreadyExistsMessage = "\nA category with this name already exists. Try again!\n";

        public static readonly string SuccessfullyAddedCategoryMessage = "\nSuccessfully added new category '{0}'!\n";

        public static readonly string CategoryToDeleteNameMessage = "\nEnter the name of the category you wish to delete:\nNOTE: All contacts related to it will be deleted!\n";

        public static readonly string SuccessfullyDeletedCategoryMessage = "\nSuccessfully deleted category '{0}'!\n";

        public static readonly string CategoryToUpdateNameMessage = "\nEnter the name of the category you wish to update:\n";

        public static readonly string SuccessfullyUpdatedCategoryMessage = "\nSuccessfully updated category!\n";

        public static readonly string EmailMessage = "\nEnter the message you wish to send to the contact's email:\n";
    }
}

