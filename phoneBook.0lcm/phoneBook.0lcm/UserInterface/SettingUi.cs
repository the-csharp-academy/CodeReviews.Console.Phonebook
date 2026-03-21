using phoneBook._0lcm.Configuration;
using phoneBook._0lcm.Enums;
using phoneBook._0lcm.ServiceContracts;
using static phoneBook._0lcm.UserInterface.DisplayHelper;

namespace phoneBook._0lcm.UserInterface;

public class SettingUi(
    ISettingService settingService,
    AppSettings appSettings,
    IVerificationService verificationService)
{
    //------- Menu Methods -------
    internal void Settings()
    {
        while (true)
        {
            Console.Clear();
            var userEmailDescription =
                $"* Email Address - this is required to use the app's emailing function | Currently: {appSettings.UserEmail ?? "Not set"}";
            var userAppPasswordDescription =
                $"* Email App Password - this is required to log into your email, and should be the APP password, not account password. | Currently: {appSettings.UserAppPassword ?? "Not set"}";
            var userDisplayNameDescription =
                $"* Display Name - this will be used when sending emails | Currently: {appSettings.UserName ?? "Not set"}";
            var textBeltKeyDescription =
                $"* Textbelt Key - this is used to send an sms message from within the app. You can read this application's readme for more details. | Currently: {appSettings.TextBeltKey}";

            DisplayMessage(
                $"\n{userEmailDescription}\n\n{userAppPasswordDescription}\n\n{userDisplayNameDescription}\n\n{textBeltKeyDescription}");

            var option = DisplayMenu<SettingsMenuOption>("\nPlease select an option:");

            var shouldReturn = HandleMenuOption(option);
            if (shouldReturn) return;
        }
    }

    /// <summary>
    ///     Handles the menu option
    /// </summary>
    /// <param name="option"></param>
    /// <returns>true if the menu should return, else false</returns>
    private bool HandleMenuOption(SettingsMenuOption option)
    {
        switch (option)
        {
            case SettingsMenuOption.SetEmailAddress:
                SetEmailAddress();
                break;
            case SettingsMenuOption.SetAppPassword:
                SetUserAppPassword();
                break;
            case SettingsMenuOption.SetDisplayName:
                SetUserName();
                break;
            case SettingsMenuOption.SetTextbeltKey:
                SetTextBeltKey();
                break;
            case SettingsMenuOption.ResetSettings:
                ResetSettings();
                break;
            case SettingsMenuOption.Back:
                return true;
        }

        return false;
    }

    //------- Setting Methods -------
    private void ResetSettings()
    {
        settingService.ResetSettings();
        DisplaySuccess("Settings have been reset.");
        WaitForUser();
    }

    private void SetEmailAddress()
    {
        while (true)
        {
            Console.Clear();
            var userEmail = DisplayQuestion("Please enter your email address:");

            if (!verificationService.ValidateEmail(userEmail))
            {
                DisplayWarning(
                    "This email address is not valid. Please make sure you have a body, '@', and valid domain address.");
                WaitForUser();
                continue;
            }

            settingService.SetUserEmailAddress(userEmail);
            DisplaySuccess("Successfully set email address.");
            WaitForUser();

            return;
        }
    }

    private void SetUserAppPassword()
    {
        Console.Clear();
        var userAppPassword = DisplayQuestion("Please enter your email's app password (NOT your account password):");

        settingService.SetUserPassword(userAppPassword);

        DisplaySuccess("Successfully set app password.");
        WaitForUser();
    }

    private void SetUserName()
    {
        Console.Clear();

        var userName = DisplayQuestion("Please enter your display name:");

        settingService.SetUserEmailDisplayName(userName);

        DisplaySuccess("Successfully set username.");
        WaitForUser();
    }

    private void SetTextBeltKey()
    {
        Console.Clear();

        var key = DisplayQuestion(
            "Please enter your paid Textbelt key, enter 'textbelt' for a free demo (1 sms per day), or 'textbelt_test' for testing purposes.");

        settingService.SetTextBeltKey(key);

        DisplaySuccess("Successfully set textbelt key.");
        WaitForUser();
    }

    //------- Helper Methods -------
    private void WaitForUser()
    {
        DisplayInfo("Press enter to continue.");
        Console.ReadLine();
    }
}