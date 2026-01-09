using PhoneBook.TerrenceLGee.Entities;
using PhoneBook.TerrenceLGee.Extensions.Pagination;

namespace PhoneBook.TerrenceLGee.Data.Rrepositories.Interfaces;

public interface IPhonebookRepository
{
    Task<bool> AddContactAsync(Contact contact);
    Task<bool> UpdateContactAsync(Contact contact);
    Task<bool> DeleteContactAsync(int contactId);
    Task<Contact?> GetContactAsync(int contactId);
    Task<PagedList<Contact>> GetContactsAsync(int page, int pageSize);
    Task<int> GetContactCountAsync();
}
