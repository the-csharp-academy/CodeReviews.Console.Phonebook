using PhoneBook.davetn657.Controllers;

namespace Phonebook.davetn657.Tests
{
    public class ValidationTests
    {
        Validation validationController = new Validation();

        [Fact]
        public void ValidateEmail_ValidTest()
        {
            var expected = true;

            var actual = validationController.ValidateEmail("tempemail@gmail.com");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateEmail_ValidSpecialCharacters()
        {
            var expected = true;

            var actual = validationController.ValidateEmail("temp.email!#$^%&*@gmail.com");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateEmail_InvalidTest()
        {
            var expected = false;

            var actual = validationController.ValidateEmail("thisIsnotanemail.com");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidatePhone_ValidTest()
        {
            var expected = true;

            var actual = validationController.ValidatePhoneNumber("123-456-7890");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidatePhone_InvalidTooLong()
        {
            var expected = false;

            var actual = validationController.ValidatePhoneNumber("12-456-7890");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidatePhoneNumber_InvalidTooShort()
        {
            var expected = false;

            var actual = validationController.ValidatePhoneNumber("12345-456-7890");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidatePhone_InvalidTest()
        {
            var expected = false;

            var actual = validationController.ValidatePhoneNumber("1234567890");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidatePhone_InvalidSpaces()
        {
            var expected = false;

            var actual = validationController.ValidatePhoneNumber("123 456 7890");

            Assert.Equal(expected, actual);
        }
    }
}