using PhoneBook.TerrenceLGee.DTOs.ContactDTOs;
using PhoneBook.TerrenceLGee.Extensions.Pagination;

namespace PhoneBook.TerrenceLGee.Services.Interfaces;

public interface IPhonebookService
{
    Task<bool> AddContactAsync(CreateContactDto dto);
    Task<bool> UpdateContactAsync(UpdateContactDto dto);
    Task<bool> DeleteContactAsync(int contactId);
    Task<RetrievedContactDto?> GetContactAsync(int contactId);
    Task<PagedList<RetrievedContactDto>> GetContactsAsync(int page, int pageSize);
    Task<int> GetContactCountAsync();
}
