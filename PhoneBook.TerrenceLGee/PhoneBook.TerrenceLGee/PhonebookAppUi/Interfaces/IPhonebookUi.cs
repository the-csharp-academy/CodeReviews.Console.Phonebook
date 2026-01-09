namespace PhoneBook.TerrenceLGee.PhonebookAppUi.Interfaces;

public interface IPhonebookUi
{
    Task AddContactAsync();
    Task UpdateContactAsync();
    Task DeleteContactAsync();
    Task ViewContactAsync();
    Task ViewAllContactsAsync();
    Task SendEmailAsync();
}
