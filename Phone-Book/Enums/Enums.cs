using System.ComponentModel.DataAnnotations;

namespace Phone_Book.Models;

public class Enums
{
    public enum CategoryMenuOptions
    {
        [Display(Name = "Add Category")]
        AddCategory,

        [Display(Name = "View All Categories")]
        ViewAllCategories,

        [Display(Name = "Edit Category")]
        EditCategory,

        [Display(Name = "Delete Category")]
        DeleteCategory,

        [Display(Name = "Back to Main Menu")]
        BackToMainMenu,
    }

    public enum ContactMenuOptions
    {
        [Display(Name = "Add Contact")]
        AddContact,

        [Display(Name = "View Contact")]
        ViewContact,

        [Display(Name = "View All Contacts")]
        ViewAllContacts,

        [Display(Name = "Edit Contact")]
        EditContact,

        [Display(Name = "Delete Contact")]
        DeleteContact,

        [Display(Name = "Back to Main Menu")]
        BackToMainMenu,
    }

    public enum MainMenuOptions
    {
        [Display(Name = "Manage Categories")]
        ManageCategories,

        [Display(Name = "Manage Contacts")]
        ManageContacts,

        [Display(Name = "Send An Email")]
        SendEmail,

        [Display(Name = "Quit")]
        Quit,
    }
}
