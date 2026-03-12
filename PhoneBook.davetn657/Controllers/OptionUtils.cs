using System.ComponentModel;
using System.Reflection;

namespace PhoneBook.davetn657.Controllers;
public class OptionUtils
{
    public OptionUtils()
    {

    }

    public string GetStringValue(Enum value)
    {
        var info = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if(attributes.Length > 0)
        {
            return attributes[0].Description;
        }
        return string.Empty;
    }

    public List<string> GetAllStringValues(Type enumType)
    {
        var enumValues = enumType.GetEnumValues();
        var allValues = new List<string>();

        foreach (var value in enumValues)
        {
            allValues.Add(GetStringValue((Enum)value));
        }

        return allValues;
    }

    public Enum GetEnumValue(string description, Type enumType)
    {
        var enumValues = enumType.GetEnumValues();

        foreach(var value in enumValues)
        {
            if (GetStringValue((Enum)value).Equals(description))
            {
                return (Enum) value;
            }
        }
        return null;
    }
}