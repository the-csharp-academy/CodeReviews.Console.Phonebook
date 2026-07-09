using PhoneBook.Menus;

namespace PhoneBook.UnitTest;

public class Tests
{
    private static readonly TestCaseData[] NameTestCases =
        {
            new("Miguel", true),
            new("Miguel Angel", true),
            new("José", true),
            new("María Fernanda", true),
            new("Álvaro", true),
            new("Ñuñez", true),

            new("", false),
            new(" ", false),
            new((string?)null, false),

            new("12345", false),
            new("Miguel123", false),
            new("@Miguel", false),
            new("Miguel!", false),

            new("A", false),
            new(new string('A', 101), false)
        };

    public static readonly TestCaseData[] EmailTestCases =
        {
            new("usuario@correo.com", true),
            new("miguel.ospina@gmail.com", true),
            new("miguel_ospina@outlook.com", true),
            new("miguel-ospina@hotmail.com", true),
            new("usuario123@empresa.co", true),
            new("nombre+alias@gmail.com", true),
            new("usuario@mail.example.com", true),

            new("", false),
            new(" ", false),
            new((string?)null, false),

            new("usuario", false),
            new("usuario@", false),
            new("@correo.com", false),
            new("usuario@correo", false),
            new("usuario@.com", false),
            new("usuario@correo.", false),
            new("usuario@@correo.com", false),
            new("usuario correo@correo.com", false),
            new("usuario@correo .com", false),
            new("usuario@correo,com", false),
            new("usuario@correo..com", false)
        };

    public static readonly TestCaseData[] InternationalPhonesTestCases =
        {
            // America
            new("+573001234567", true),      // Colombia
            new("+573105551234", true),
            new("+12025550123", true),       // United States
            new("+14155552671", true),       // United States
            new("+15145551234", true),       // Canadá
            new("+525512345678", true),      // México
            new("+5491123456789", true),     // Argentina
            new("+5511987654321", true),     // Brasil
            new("+56991234567", true),       // Chile
            new("+51987654321", true),       // Perú

            // Europe
            new("+34612345678", true),       // Spain
            new("+33123456789", true),       // France
            new("+393331234567", true),      // Italy
            new("+4915112345678", true),     // German
            new("+447911123456", true),      // United Kingdom
            new("+351912345678", true),      // Portugal
            new("+31612345678", true),       // Netherlands

            // Asia
            new("+819012345678", true),      // Japan
            new("+8613812345678", true),     // China
            new("+919876543210", true),      // India
            new("+6591234567", true),        // Singapore
            new("+821012345678", true),      // South Korea

            // Oceanía
            new("+61412345678", true),       // Australia
            new("+64211234567", true),       // New Zealand

            // Africa
            new("+27821234567", true),       // South Africa

            // Wrong Inputs
            new("", false),
            new(" ", false),
            new((string?)null, false),
            new("573001234567", false),      // Without +
            new("+57", false),
            new("+123", false),
            new("+999999999999999999", false),
            new("+57ABC123456", false),
            new("+57 3001234567", false),
            new("+57-3001234567", false),
            new("++573001234567", false)
        };

    public static readonly TestCaseData[] CategoryNameTestCases =
        {
            // Valid
            new("Family", true),
            new("Friends", true),
            new("Group 1", true),
            new("Category 2026", true),
            new("VIP 1", true),
            new("Room-2", true),
            new("Section 10", true),
            new("Level 3", true),
            new("Work_2",true),

            // Empty
            new("", false),
            new(" ", false),
            new((string?)null, false),

            // Too short / too long
            new("A", false),
            new(new string('A', 51), false),

            // Invalid characters
            new("Family@", false),
            new("Group#", false),
            new("VIP$", false),
            new("Room%", false),
            new("Category!", false),
            new("Test&1", false),
            new("Office*", false),
            new("Home+", false)
        };

    [TestCaseSource(nameof(NameTestCases))]
    public void IsValidName_ReturnsExpectedResult(string name, bool expedtedResult)
    {
        var isValidName = ValidateInput.IsValidName(name);

        Assert.That(isValidName, Is.EqualTo(expedtedResult));
    }

    [TestCaseSource(nameof(EmailTestCases))]
    public void IsValidEmail_ReturnsExpectedResult(string email, bool expedtedResult)
    {
        var isValidEmail = ValidateInput.IsValidEmail(email);

        Assert.That(isValidEmail, Is.EqualTo(expedtedResult));
    }

    [TestCaseSource(nameof(InternationalPhonesTestCases))]
    public void IsValidPhone_ReturnsExpectedResult(string phone,bool expectedResult)
    {
        var isValidPhoneNumber = ValidateInput.IsValidPhoneNumber(phone);

        Assert.That(isValidPhoneNumber, Is.EqualTo(expectedResult));
    }

    [TestCaseSource(nameof(CategoryNameTestCases))]
    public void IsValidCategoryName_ReturnsExpectedResult(string categoryName, bool expectedResult) 
    {
        var isValidCategoryName = ValidateInput.IsValidCategoryName(categoryName);

        Assert.That(isValidCategoryName, Is.EqualTo(expectedResult));
    }
}