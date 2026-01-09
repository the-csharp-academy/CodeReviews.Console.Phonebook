using PhoneBook.TerrenceLGee.Data.Rrepositories.Interfaces;
using PhoneBook.TerrenceLGee.DTOs.ContactDTOs;
using PhoneBook.TerrenceLGee.Services.Interfaces;
using PhoneBook.TerrenceLGee.Mappings.ContactMappings;
using PhoneBook.TerrenceLGee.Extensions.Pagination;

namespace PhoneBook.TerrenceLGee.Services;

public class PhonebookService : IPhonebookService
{
    private readonly IPhonebookRepository _repository;

    public PhonebookService(IPhonebookRepository repository) => _repository = repository;

    public async Task<bool> AddContactAsync(CreateContactDto dto)
    {
        return await _repository.AddContactAsync(dto.FromCreateContactDto());
    }

    public async Task<bool> UpdateContactAsync(UpdateContactDto dto)
    {
        return await _repository.UpdateContactAsync(dto.FromUpdateContactDto());
    }

    public async Task<bool> DeleteContactAsync(int contactId)
    {
        return await _repository.DeleteContactAsync(contactId);
    }

    public async Task<RetrievedContactDto?> GetContactAsync(int contactId)
    {
        var contact = await _repository.GetContactAsync(contactId);
        return contact?.ToRetrievedContactDto();
    }

    public async Task<PagedList<RetrievedContactDto>> GetContactsAsync(int page,int pageSize)
    {
        var pagedContacts = await _repository.GetContactsAsync(page, pageSize);
        return pagedContacts.ToRetrievedContactDtoPagedList(page, pageSize);
    }

    public async Task<int> GetContactCountAsync()
    {
        return await _repository.GetContactCountAsync();
    }
}
