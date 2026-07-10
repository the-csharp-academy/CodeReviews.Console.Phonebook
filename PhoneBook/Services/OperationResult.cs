namespace PhoneBook.Services;

public sealed record OperationResult(bool Success, string Message)
{
    public static OperationResult Succeeded(string message) => new(true, message);

    public static OperationResult Failed(string message) => new(false, message);
}
