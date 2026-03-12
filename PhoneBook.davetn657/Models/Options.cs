using System.ComponentModel;

public enum MainMenuOptions
{
    [Description("Exit")]Exit,
    [Description("Add Contact")]AddContacts
}

public enum AddNewContactOptions
{
    [Description("Return")]Return,
    [Description("Phone Number")]PhoneNumber,
    [Description("Email")]Email,
    [Description("Both")]Both
}

public enum EditContactOptions
{
    [Description("Return")] Return,
    [Description("Send Email")] SendEmail,
    [Description("Add to Group")] AddGroup,
    [Description("Change Phone Number")] ChangePhone,
    [Description("Change Email")] ChangeEmail,
    [Description("Change Name")] ChangeName,
    [Description("Delete")] Delete
}

public enum AddCategoryOptions
{
    [Description("Return")] Return,
    [Description("Create new category")] CreateCategory
}