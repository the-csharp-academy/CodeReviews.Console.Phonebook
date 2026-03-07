namespace PhoneBook.Models;
public class Enums
{
    public enum MenuOptions
    {
        AddUser,
        RemoveUser,
        UpdateUser,
        GetUser,
        GetAllUsers,
        GetUsersByCategory,
        SendAnEmail,
        Exit,
    }

    public enum UserCategories
    {
        Family,
        Friends,
        Work,
        Other
    }
}
