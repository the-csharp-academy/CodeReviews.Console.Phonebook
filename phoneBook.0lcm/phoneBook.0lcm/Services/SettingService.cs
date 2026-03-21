using phoneBook._0lcm.Configuration;
using phoneBook._0lcm.ServiceContracts;

namespace phoneBook._0lcm.Services;

public class SettingService(AppSettings appSettings, SettingsManager settingsManager) : ISettingService
{
    public void ResetSettings()
    {
        appSettings.UserEmail = appSettings.UserEmailDefault;
        appSettings.UserAppPassword = appSettings.UserAppPasswordDefault;
        appSettings.UserName = appSettings.UserName;
        appSettings.TextBeltKey = appSettings.TextBeltKeyDefault;

        settingsManager.Save(appSettings);
    }

    public void SetUserEmailAddress(string email)
    {
        appSettings.UserEmail = email;

        settingsManager.Save(appSettings);
    }

    public void SetUserPassword(string password)
    {
        appSettings.UserAppPassword = password;

        settingsManager.Save(appSettings);
    }

    public void SetUserEmailDisplayName(string name)
    {
        appSettings.UserName = name;

        settingsManager.Save(appSettings);
    }

    public void SetTextBeltKey(string key)
    {
        appSettings.TextBeltKey = key;

        settingsManager.Save(appSettings);
    }
}