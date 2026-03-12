using PhoneBook.davetn657.Controllers;

namespace Phonebook.davetn657.Tests
{
    public class OptionUtilsTests
    {
        OptionUtils optionUtilsController = new OptionUtils();

        [Fact]
        public void GetStringValue_ValidTest()
        {
            var expected = "Add to Group";

            var actual = optionUtilsController.GetStringValue(EditContactOptions.AddGroup);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetStringValue_InvalidTest()
        {
            var expected = "Add to Group";

            var actual = optionUtilsController.GetStringValue(EditContactOptions.AddGroup);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllStringValues_ValidTest()
        {
            var expected = new string[]
            {
                "Exit",
                "Add Contact"
            };

            var actual = optionUtilsController.GetAllStringValues(typeof(MainMenuOptions));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEnumValue_ValidTest()
        {
            var expected = AddNewContactOptions.PhoneNumber;

            var actual = optionUtilsController.GetEnumValue("Phone Number", typeof(AddNewContactOptions));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEnumValue_InvalidTest()
        {
            AddNewContactOptions? expected = null;

            var actual = optionUtilsController.GetEnumValue("Check Email", typeof(AddNewContactOptions));

            Assert.Equal(expected, actual);
        }
    }
}
