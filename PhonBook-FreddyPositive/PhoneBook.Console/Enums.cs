namespace PhoneBook;

internal class Enums
{
    internal enum PhoneBookMenu
    {
        AddContact,
        ViewAllContacts,
        SearchContact,
        UpdateContact,
        DeleteContact,
        Exit
    }
    internal enum UserInput
    {
        NameOnly,
        EmailOnly,
        PhoneNumberOnly,
        NameAndEmail,
        NameAndPhone,
        EmailAndPhone,
        NameAndEmailAndPhone,
        ExitToMainMenu
    }
}
