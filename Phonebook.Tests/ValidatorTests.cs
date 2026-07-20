using Xunit;
using Phonebook;

namespace Phonebook.Tests
{
    public class ValidatorTests
    {
        [Theory]
        [InlineData("+12123456789", true)]
        [InlineData("+84912345678", true)]
        [InlineData("1234567890", true)]
        [InlineData("+54911234567", true)]
        [InlineData("987654321", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData("abc", false)]
        [InlineData("+012345", false)]
        [InlineData("+12345678901234567", false)]
        [InlineData("+", false)]
        [InlineData("123-456-7890", false)]
        public void IsValidNumber_ShouldReturnExpectedResult(string? phoneNumber, bool expected)
        {
            bool result = Validator.IsValidNumber(phoneNumber);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("messi@goat.com", true)]
        [InlineData("max33@redbull.com", true)]
        [InlineData("abc@gmail.com", true)]
        [InlineData("john.doe@company.co.uk", true)]
        [InlineData("user_123@sub.domain.org", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData("plainaddress", false)]
        [InlineData("@domain.com", false)]
        [InlineData("user@", false)]
        [InlineData("user@domain", false)]
        [InlineData("user@.com", false)]
        public void IsValidEmail_ShouldReturnExpectedResult(string? email, bool expected)
        {
            bool result = Validator.IsValidEmail(email);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0", 5, true)]
        [InlineData("1", 5, true)]
        [InlineData("3", 5, true)]
        [InlineData("5", 5, true)]
        [InlineData(null, 5, false)]
        [InlineData("", 5, false)]
        [InlineData("   ", 5, false)]
        [InlineData("-1", 5, false)]
        [InlineData("6", 5, false)]
        [InlineData("10", 5, false)]
        [InlineData("abc", 5, false)]
        [InlineData("1.5", 5, false)]
        public void IsValidChoice_ShouldReturnExpectedResult(string? strInput, int maxNum, bool expected)
        {
            bool result = Validator.IsValidChoice(strInput, maxNum);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("John Doe", true)]
        [InlineData("Football", true)]
        [InlineData("Family", true)]
        [InlineData("Category 1", true)]
        [InlineData("Name/Title", true)]
        [InlineData("12345", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("John@Doe", false)]
        [InlineData("Test!", false)]
        [InlineData("Hello#World", false)]
        [InlineData("<script>", false)]
        [InlineData("user$name", false)]
        public void IsValidString_ShouldReturnExpectedResult(string? input, bool expected)
        {
            bool result = Validator.IsValidString(input);

            Assert.Equal(expected, result);
        }
    }
}
