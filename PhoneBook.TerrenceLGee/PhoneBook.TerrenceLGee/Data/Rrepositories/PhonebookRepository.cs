using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.TerrenceLGee.Data.Rrepositories.Interfaces;
using PhoneBook.TerrenceLGee.Entities;
using PhoneBook.TerrenceLGee.Extensions.Pagination;

namespace PhoneBook.TerrenceLGee.Data.Rrepositories;

public class PhonebookRepository : IPhonebookRepository
{
    private readonly PhonebookContext _context;
    private readonly ILogger<PhonebookRepository> _logger;
    private string _errorMessage = string.Empty;

    public PhonebookRepository(
        PhonebookContext context,
        ILogger<PhonebookRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> AddContactAsync(Contact contact)
    {
        try
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(AddContactAsync)) +
                $"There was an error updating the database when attempting to add new contact: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return false;
        }
        catch (Exception ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(AddContactAsync)) +
                $"There was an unexpected error updating the database when attempting to add new contact: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return false;
        }
    }

    public async Task<bool> UpdateContactAsync(Contact contact)
    {
        try
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(UpdateContactAsync)) +
                $"There was an error updating the database when attempting to update the contact: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return false;
        }
        catch (Exception ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(UpdateContactAsync)) +
                $"There was an unexpected error updating the database when attempting to update the contact: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return false;
        }
    }

    public async Task<bool> DeleteContactAsync(int contactId)
    {
        try
        {
            if (contactId <= 0) return false;

            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);

            if (contact is null) return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (DbUpdateException ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(DeleteContactAsync)) +
                $"There was an error updating the database when attempting to delete the contact {contactId}: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return false;
        }
        catch (Exception ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(DeleteContactAsync)) +
                $"There was an unexpected error updating the database when attempting to delete the contact {contactId}: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return false;
        }
    }

    public async Task<Contact?> GetContactAsync(int contactId)
    {
        try
        {
            return await _context.Contacts
                .Where(c => c.Id == contactId)
                .Include(c => c.PhoneNumbers)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        catch (DbUpdateException ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(GetContactAsync)) +
                $"There was an error accessing the database when attempting to retrieve the contact {contactId}: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return null;
        }
        catch (Exception ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(GetContactAsync)) +
                $"There was an unexpected error accessing the database when attempting to retrieve the contact {contactId}: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return null;
        }
    }

    public async Task<PagedList<Contact>> GetContactsAsync(int page, int pageSize)
    {
        try
        {
            var count = await GetContactCountAsync();

            return await _context.Contacts
                .Include(c => c.PhoneNumbers)
                .AsNoTracking()
                .ToPagedListAsync(count, page, pageSize);
        }
        catch (DbUpdateException ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(GetContactsAsync)) +
                $"There was an error accessing the database when attempting to retrieve the contacts: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return [];
        }
        catch (Exception ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(GetContactsAsync)) +
                $"There was an unexpected error accessing the database when attempting to retrieve the contacts: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return [];
        }
    }

    public async Task<int> GetContactCountAsync()
    {
        try
        {
            return await _context.Contacts
                .AsNoTracking()
                .CountAsync();
        }
        catch (DbUpdateException ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(GetContactCountAsync)) +
                $"There was an error accessing the database when attempting to retrieve the count of contacts: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return 0;
        }
        catch (Exception ex)
        {
            _errorMessage = GenerateErrorMessage(nameof(PhonebookRepository), nameof(GetContactCountAsync)) +
                $"There was an unexpected error accessing the database when attempting to retrieve the count of contacts: {ex.Message}\n";
            _logger.LogError(ex, "{msg}\n\n", _errorMessage);
            return 0;
        }
    }

    private string GenerateErrorMessage(string className, string methodName)
    {
        return $"\nClass: {className}\n" +
            $"Method: {methodName}\n";
    }

    
}
