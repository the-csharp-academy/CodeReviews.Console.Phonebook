using System.ComponentModel;

namespace PhoneBook.m1chael888.Enums
{
    public static class ContactViewEnums
    {
        public enum MainMenuOption
        {
            [Description("Add a contact")]
            CreateContact,
            [Description("View Contacts")]
            ReadContacts,
            [Description("Edit a contact")]
            UpdateContact,
            [Description("Delete a contact")]
            DeleteContact,
            Exit
        }
    }
}
