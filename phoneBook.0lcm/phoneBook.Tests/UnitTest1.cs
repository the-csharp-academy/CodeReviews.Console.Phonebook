using phoneBook._0lcm.ServiceContracts;
using phoneBook._0lcm.Services;

namespace phoneBook.Tests;

public class Tests
{
    private readonly IVerificationService _verificationService = new VerificationService();

    [TestCase("JohnDoe@Gmail.com", true)]
    [TestCase("JohnDoe@gmail.com", true)]
    [TestCase("John.Doe@Gmail.com", true)]
    [TestCase("John-Doe@Gmail.com", true)]
    [TestCase("John_Doe@Gmail.com", true)]
    [TestCase("JohnDoe1000@Gmail.com", true)]
    [TestCase("J@Gmail.com", true)]
    [TestCase("JohnDoe@Hotmail.com", true)]
    [TestCase("JOHNDOE@GMAIL.COM", true)]
    [TestCase("johndoe@gmail.com", true)]
    [TestCase("@Gmail.com", false)]
    [TestCase("JohnDoeGmail.com", false)]
    [TestCase("JohnDoe@", false)]
    [TestCase("John Doe <johnDoe@gmail.com>", false)]
    [TestCase("", false)]
    [TestCase(null, false)]
    [TestCase("John Doe@Gmail.com", false)]
    [TestCase("JohnDoe@ gmail.com", false)]
    public void VerifyEmail_GivenValidInput_ReturnCorrectBool(string? input, bool expected)
    {
        bool? result;

        try
        {
            result = _verificationService.ValidateEmail(input);
        }
        catch (Exception)
        {
            result = null;
        }

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("+1 (312) 555-0175", true)]
    [TestCase("+1 312 555 0175", true)]
    [TestCase("+1 312-555-0175", true)]
    [TestCase("+1 312555-0175", true)]
    [TestCase("+44 20 7946 0958", true)]
    [TestCase("+44 121 496 0329", true)]
    [TestCase("+20 2 2345 6789", true)]
    [TestCase("+216 71 234 567", true)]
    [TestCase("+216 98 123 456", true)]
    [TestCase("+216 98123456", true)]
    [TestCase("", false)]
    [TestCase(null, false)]
    [TestCase("(312)-555-0175", false)]
    [TestCase("3125550175", false)]
    [TestCase("+1 12345678912345", false)]
    [TestCase("+216 (312)-555-0175", false)]
    public void VerifyPhoneNumber_GivenValidNumber_ReturnCorrectBool(string? input, bool expected)
    {
        bool? result;

        try
        {
            result = _verificationService.ValidatePhoneNumber(input, "ZZ");
        }
        catch (Exception)
        {
            result = null;
        }

        Assert.That(result, Is.EqualTo(expected));
    }
}