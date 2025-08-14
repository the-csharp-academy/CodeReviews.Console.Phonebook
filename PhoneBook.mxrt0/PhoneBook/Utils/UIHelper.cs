using PhoneBook.Utils.Enums;
using System.Text.RegularExpressions;

namespace PhoneBook.Utils;

public static class UIHelper
{
    public static void DisplayOptions()
    {
        foreach (var item in Enum.GetValues(typeof(MenuOption)))
        {
            Console.WriteLine($"\nType {(int)item} to{Regex.Replace(Enum.GetName((MenuOption)item)!, "([A-Z])", " $1")}");
        }
        Console.WriteLine();
        Console.Write("Your input: ");
    }
}
