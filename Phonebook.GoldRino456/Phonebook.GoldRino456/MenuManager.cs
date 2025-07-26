
using PhoneBook.GoldRino456.Data;
using Utilities.GoldRino456;

namespace PhoneBook.GoldRino456;

public static class MenuManager
{
    public static void ProcessCreateContact(string connectionString)
    {
        ContactContext context = new(connectionString);
        ContactEntry contact = new();
        DisplayUtils.ClearScreen();
        MenuHelper.CollectContactDetails(contact, context, true);
    }

    public static void ProcessUpdateContact(string connectionString)
    {
        ContactContext context = new(connectionString);
        DisplayUtils.ClearScreen();
        var isCancellingOperation = MenuHelper.PromptUserForContactSelection(context, out ContactEntry? selectedContact);

        if (isCancellingOperation)
        {
            return;
        }

        MenuHelper.DisplaySingleContact(selectedContact);
        MenuHelper.CollectContactDetails(selectedContact, context, false);
    }

    public static void ProcessViewContacts(string connectionString)
    {
        ContactContext context = new(connectionString);
        DisplayUtils.ClearScreen();
        var contacts = MenuHelper.GetContactsByCategory(context);

        string[] columns = ["Name", "Email", "Phone Number", "Category"];
        List<string[]> rows = new();

        foreach (var contact in contacts)
        {
            var categoryString = contact.Category != null ? contact.Category.Name : "N/A";
            string[] row = [contact.Name, contact.Email, contact.PhoneNumber, categoryString];
            rows.Add(row);
        }

        DisplayUtils.DisplayListAsTable(columns, rows);
        DisplayUtils.PressAnyKeyToContinue();
    }

    public static void ProcessDeleteContact(string connectionString)
    {
        ContactContext context = new(connectionString);
        DisplayUtils.ClearScreen();
        var isCancellingOperation = MenuHelper.PromptUserForContactSelection(context, out ContactEntry? selectedContact);

        if (isCancellingOperation)
        {
            return;
        }

        DisplayUtils.ClearScreen();
        MenuHelper.DisplaySingleContact(selectedContact);
        var isDeleteConfirmed = DisplayUtils.PromptUserForYesOrNoSelection("Are you sure you want to delete this contact?");

        if (isDeleteConfirmed)
        {
            context.Contacts.Remove(selectedContact);
            context.SaveChanges();
        }
    }

    public static void ProcessCreateCategory(string connectionString)
    {
        ContactContext context = new(connectionString);
        DisplayUtils.ClearScreen();

        ContactCategory category = new();
        bool isCancellingOperation = false;

        isCancellingOperation = MenuHelper.ProcessCategoryInput(category);

        if (isCancellingOperation)
        {
            return;
        }

        if (MenuHelper.ConfirmCategoryDetails(category, context, true))
        {
            return;
        }
    }

    public static void ProcessUpdateCategory(string connectionString)
    {
        ContactContext context = new(connectionString);
        DisplayUtils.ClearScreen();

        if (!MenuHelper.CheckForExistingCategories(context))
        {
            DisplayUtils.DisplayMessageToUser("No categories exist to update!");
            DisplayUtils.PressAnyKeyToContinue();
            return;
        }

        var category = MenuHelper.PromptUserForCategorySelection(context, "Go Back To Menu");
        if (category == null)
        {
            return;
        }

        MenuHelper.DisplaySingleCategory(category);
        var isCancellingOperation = MenuHelper.ProcessCategoryInput(category);

        if (isCancellingOperation)
        {
            return;
        }

        if (MenuHelper.ConfirmCategoryDetails(category, context, false))
        {
            return;
        }
    }

    public static void ProcessDeleteCategory(string connectionString)
    {
        ContactContext context = new(connectionString);
        DisplayUtils.ClearScreen();
        var selectedCategory = MenuHelper.PromptUserForCategorySelection(context, "Go Back To Menu");

        if (selectedCategory == null)
        {
            return;
        }

        DisplayUtils.ClearScreen();
        MenuHelper.DisplaySingleCategory(selectedCategory);
        var isDeleteConfirmed = DisplayUtils.PromptUserForYesOrNoSelection("Are you sure you want to delete this category?");

        if (isDeleteConfirmed)
        {
            context.ContactCategories.Remove(selectedCategory);
            context.SaveChanges();
        }
    }

}
