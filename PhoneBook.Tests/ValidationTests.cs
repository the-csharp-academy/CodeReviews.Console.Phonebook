using PhoneBook.Helpers;

namespace PhoneBook.Tests;

public class ValidationTests
{
    // ── IsValidPhoneNumber ──────────────────────────────────────

    [Theory]
    [InlineData("+1-555-0101")]
    [InlineData("5550101")]
    [InlineData("(555) 010-1234")]
    [InlineData("+44 20 7946 0958")]
    [InlineData("1234567")]
    [InlineData("+1 (800) 555-0199")]
    public void IsValidPhoneNumber_ValidFormats_ReturnsTrue(string phone)
    {
        Assert.True(Validation.IsValidPhoneNumber(phone));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    [InlineData("12345")]
    [InlineData("123456789012345678901")]
    [InlineData("phone: 123")]
    public void IsValidPhoneNumber_InvalidFormats_ReturnsFalse(string phone)
    {
        Assert.False(Validation.IsValidPhoneNumber(phone));
    }

    [Fact]
    public void IsValidPhoneNumber_Null_ReturnsFalse()
    {
        Assert.False(Validation.IsValidPhoneNumber(null!));
    }

    // ── IsValidEmail ────────────────────────────────────────────

    [Theory]
    [InlineData("user@example.com")]
    [InlineData("name@sub.domain.com")]
    [InlineData("test.user@mail.org")]
    [InlineData("a@b.co")]
    public void IsValidEmail_ValidFormats_ReturnsTrue(string email)
    {
        Assert.True(Validation.IsValidEmail(email));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("userexample.com")]
    [InlineData("user@")]
    [InlineData("user@ example.com")]
    [InlineData("@example.com")]
    public void IsValidEmail_InvalidFormats_ReturnsFalse(string email)
    {
        Assert.False(Validation.IsValidEmail(email));
    }

    [Fact]
    public void IsValidEmail_Null_ReturnsFalse()
    {
        Assert.False(Validation.IsValidEmail(null!));
    }

    // ── IsValidName ─────────────────────────────────────────────

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob Johnson")]
    [InlineData("A")]
    public void IsValidName_ValidNames_ReturnsTrue(string name)
    {
        Assert.True(Validation.IsValidName(name));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void IsValidName_EmptyOrWhitespace_ReturnsFalse(string name)
    {
        Assert.False(Validation.IsValidName(name));
    }

    [Fact]
    public void IsValidName_Null_ReturnsFalse()
    {
        Assert.False(Validation.IsValidName(null!));
    }

    [Fact]
    public void IsValidName_Over100Chars_ReturnsFalse()
    {
        var longName = new string('A', 101);
        Assert.False(Validation.IsValidName(longName));
    }

    [Fact]
    public void IsValidName_Exactly100Chars_ReturnsTrue()
    {
        var name = new string('A', 100);
        Assert.True(Validation.IsValidName(name));
    }

    [Theory]
    [InlineData("Family")]
    [InlineData("Work Contacts")]
    public void IsValidCategoryName_ValidNames_ReturnsTrue(string name)
    {
        Assert.True(Validation.IsValidCategoryName(name));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void IsValidCategoryName_EmptyNames_ReturnsFalse(string name)
    {
        Assert.False(Validation.IsValidCategoryName(name));
    }

    [Theory]
    [InlineData("+15551234567")]
    [InlineData("+442079460958")]
    public void IsValidE164PhoneNumber_ValidNumbers_ReturnsTrue(string phoneNumber)
    {
        Assert.True(Validation.IsValidE164PhoneNumber(phoneNumber));
    }

    [Theory]
    [InlineData("5551234567")]
    [InlineData("+0123456789")]
    [InlineData("+1-555-123-4567")]
    public void IsValidE164PhoneNumber_InvalidNumbers_ReturnsFalse(string phoneNumber)
    {
        Assert.False(Validation.IsValidE164PhoneNumber(phoneNumber));
    }

    [Fact]
    public void IsValidSmsMessage_RejectsEmptyAndOverlongMessages()
    {
        Assert.False(Validation.IsValidSmsMessage(""));
        Assert.False(Validation.IsValidSmsMessage(new string('a', 1601)));
        Assert.True(Validation.IsValidSmsMessage("A valid message"));
    }
}
