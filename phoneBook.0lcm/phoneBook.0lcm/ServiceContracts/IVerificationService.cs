namespace phoneBook._0lcm.ServiceContracts;

public interface IVerificationService
{
    public bool ValidateEmail(string email);
    public bool ValidatePhoneNumber(string phoneNumber, string regionCode);
}