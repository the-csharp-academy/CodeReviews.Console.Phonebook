using System.ComponentModel.DataAnnotations;

namespace PhoneBook.TerrenceLGee.PhonebookAppUi.Menus;

public enum MainMenu
{
    [Display(Name = "Add a new contact")]
    AddContact,
    [Display(Name = "Update an existing contact")]
    UpdateContact,
    [Display(Name = "Delete an existing contact")]
    DeleteContact,
    [Display(Name = "View details of an existing contact")]
    ViewContact,
    [Display(Name = "View all contacts")]
    ViewAllContacts,
    [Display(Name = "Send an email to a contact")]
    SendEmail,
    [Display(Name = "Exit the program")]
    Exit
}
