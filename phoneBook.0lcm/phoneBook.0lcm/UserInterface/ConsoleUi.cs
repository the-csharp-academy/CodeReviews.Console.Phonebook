using Microsoft.Extensions.Logging;
using phoneBook._0lcm.Enums;
using static phoneBook._0lcm.UserInterface.DisplayHelper;

namespace phoneBook._0lcm.UserInterface;

internal class ConsoleUi(ContactUi contactUi, SettingUi settingUi, ILogger<ConsoleUi> logger)
{
    //------- Main Menu Methods -------
    internal async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();

            var option = DisplayMenu<MainMenuOption>();
            try
            {
                var shouldReturn = HandleMainMenu(option);
                if (!shouldReturn) continue;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Unhandled exception occurred while executing the app");

                DisplayError("An error has occurred while the app was running. Press enter to continue");
                Console.ReadLine();
                continue;
            }

            await ExitApplication();
            return;
        }
    }

    /// <summary>
    ///     Handles the main menu's selection
    /// </summary>
    /// <param name="option"></param>
    /// <returns>true if main menu should exit, else false</returns>
    private bool HandleMainMenu(MainMenuOption option)
    {
        switch (option)
        {
            case MainMenuOption.AddNewContact:
                contactUi.AddNewContact();
                break;
            case MainMenuOption.ViewContacts:
                contactUi.ViewContacts();
                break;
            case MainMenuOption.Settings:
                settingUi.Settings();
                break;
            case MainMenuOption.Exit:
                return true;
        }

        return false;
    }

    private static async Task ExitApplication()
    {
        await DisplaySpinner("Exiting application...", 2500);
        Environment.Exit(0);
    }
}