namespace phoneBook._0lcm.ServiceContracts;

public interface ISettingService
{
    public void ResetSettings();
    public void SetUserEmailAddress(string email);
    public void SetUserPassword(string password);
    public void SetUserEmailDisplayName(string name);
    public void SetTextBeltKey(string key);
}