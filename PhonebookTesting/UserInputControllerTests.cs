
using Phonebook.Controller;

namespace PhonebookTesting;

public class USerInputControllerTests
{
    private static readonly object[] IntegerTestCases =
    {
        new TestCaseData("123", true),
        new TestCaseData("Lala", true),
        new TestCaseData("This is an invalid number", true),
        new TestCaseData("$@!*%(!@%@!#!", true),
        new TestCaseData("12313NUMBER123", true),
        new TestCaseData("99493157", false),
        new TestCaseData("48911923", false),
        new TestCaseData("41249015", false)
    };

    [TestCaseSource(nameof(IntegerTestCases))]
    public void IsInvalidPhoneNumber_ReturnsTrue(string input, bool ExpectedResult)
    {
        var IsInvalidNumber = UserInputController.IsInvalidPhoneNumber(input);

        Assert.That(IsInvalidNumber, Is.EqualTo(ExpectedResult));
    }


    private static readonly object[] EmailTestCases =
    {
        new TestCaseData("foobar@example.com", false),
        new TestCaseData("foobar.example.com", true),
        new TestCaseData("#!dasads123$%!$!@example.com", false),
        new TestCaseData(null, true),
        new TestCaseData("", true)
    };
    [TestCaseSource(nameof(EmailTestCases))]
    public void IsInvalidEmailAddress_ReturnsTrue(string input, bool ExpectedResult)
    {
        var IsValidEmail = UserInputController.IsInvalidEmailAddress(input);

        Assert.That(IsValidEmail, Is.EqualTo(ExpectedResult));
    }
}
