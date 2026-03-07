namespace PhoneBook.UnitTests;

public class ValidatorTest
{
    private static readonly object[] EmailCases = {
        new TestCaseData("santi@gmail.com", true),
        new TestCaseData("jor@com", true),
        new TestCaseData("santihotmail.com", false),
        new TestCaseData("santi@gmail@.com", false),
        new TestCaseData("@gmail.com", false),
        new TestCaseData(" ggg@gmail.com ", true),
    };

    [TestCaseSource(nameof(EmailCases))]
    public void CorrectEmailInput_ReturnsTrue(string emailInput, bool expectedResult)
    {
        var isEmailValid = Validator.ValidateEmail(emailInput);

        Assert.That(isEmailValid, Is.EqualTo(expectedResult));
    }

    private static readonly object[] PhoneNumberCases = {
        new TestCaseData("+458fd56469", false),
        new TestCaseData("+45856469+", false),
        new TestCaseData("09984546", true),
        new TestCaseData("+598241022", true),
        new TestCaseData("gffdd", false),
        new TestCaseData("phone", false),
        new TestCaseData(" 098451 ", true),
        new TestCaseData("458fd56469+", false),
    };

    [TestCaseSource(nameof(PhoneNumberCases))]
    public void GivenStringInput_WhenCheckIfInputIsNumberOrHasPlusSign_ThenValueIsCorrect(string phoneNumber, bool expectedResult)
    {
        var isPhoneNumberValid = Validator.ValidatePhoneNumber(phoneNumber);

        Assert.That(isPhoneNumberValid, Is.EqualTo(expectedResult));
    }

    private static readonly object[] StringCases = {
        new TestCaseData(" ", false),
        new TestCaseData("", false),
        new TestCaseData("09984546", true),
        new TestCaseData("hola ", true),
        new TestCaseData(" hola", true),
        new TestCaseData(" phone ", true),
    };

    [TestCaseSource(nameof(StringCases))]
    public void GivenStringInput_WhenCheckIfInputIsCompatible_ThenValueIsCorrect(string input, bool expectedResult)
    {
        var isStringValid = Validator.ValidateString(input);

        Assert.That(isStringValid, Is.EqualTo(expectedResult));
    }
}
