using Spectre.Console;
using static PhoneBook.Models.Enums;

namespace PhoneBook.Views;
internal class UserInterface
{
    internal static void MainMenu()
    {
		bool isAppRunning = true;

		while (isAppRunning)
		{
			Console.Clear();

			var option = AnsiConsole.Prompt(
				new SelectionPrompt<MenuOptions>()
				.Title("Choose your menu option: ")
				.AddChoices(Enum.GetValues<MenuOptions>()));

			switch (option)
			{
				case MenuOptions.AddUser:
					UserService.AddUser();
					break;
				case MenuOptions.RemoveUser:
					UserService.RemoveUser();
					break;
				case MenuOptions.UpdateUser:
					UserService.UpdateUser();
					break;
				case MenuOptions.GetUser:
					UserService.GetUser();
					break;
				case MenuOptions.GetAllUsers:
					UserService.GetAll();
					break;
                case MenuOptions.GetUsersByCategory:
                    UserService.GetUsersBycategory();
                    break;
                case MenuOptions.SendAnEmail:
					UserService.SendEmail();
					break;
				case MenuOptions.Exit:
					isAppRunning = false;
					break;
			}
		}
	}
}
