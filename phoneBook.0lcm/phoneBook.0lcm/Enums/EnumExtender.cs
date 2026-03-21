using System.Text.RegularExpressions;

namespace phoneBook._0lcm.Enums;

internal static class EnumExtender
{
    internal static string ToDisplayString(this Enum value)
    {
        return Regex.Replace(
            value.ToString(),
            "([a-z])([A-Z])",
            "$1 $2"
        );
    }
}