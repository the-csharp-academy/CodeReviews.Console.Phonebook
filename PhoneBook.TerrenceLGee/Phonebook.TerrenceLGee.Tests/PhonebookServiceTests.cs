using Moq;
using PhoneBook.TerrenceLGee.Data.Rrepositories.Interfaces;
using PhoneBook.TerrenceLGee.DTOs.ContactDTOs;
using PhoneBook.TerrenceLGee.Entities;
using PhoneBook.TerrenceLGee.Extensions.Pagination;
using PhoneBook.TerrenceLGee.Services;
using PhoneBook.TerrenceLGee.Services.Interfaces;

namespace Phonebook.TerrenceLGee.Tests;

public class PhonebookServiceTests
{
    private readonly Mock<IPhonebookRepository> _mockRepo;
    private readonly IPhonebookService _phonebookService;
    private const int ContactId = 2;
    private const int Page = 1;
    private const int PageSize = 3;

    public PhonebookServiceTests()
    {
        _mockRepo = new Mock<IPhonebookRepository>();
        _phonebookService = new PhonebookService(_mockRepo.Object);
    }

    [Fact]
    public async Task AddContactAsync_Returns_True_WhenSuccessful()
    {
        var expectedResult = true;

        _mockRepo
            .Setup(r => r.AddContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(expectedResult);

        var newContact = Resources.GetCreateContactDto();

        var result = await _phonebookService.AddContactAsync(newContact);

        Assert.Equal(expectedResult, result);
        Assert.True(result);
    }

    [Fact]
    public async Task AddContactAsync_Returns_False_WhenFailed()
    {
        var expectedResult = false;

        _mockRepo
            .Setup(r => r.AddContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(expectedResult);

        var newContact = Resources.GetCreateContactDto();

        var result = await _phonebookService.AddContactAsync(newContact);

        Assert.Equal(expectedResult, result);
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateContactAsync_Returns_True_WhenSuccessful()
    {
        var expectedResult = true;

        _mockRepo
            .Setup(r => r.UpdateContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(expectedResult);

        var updatedContact = Resources.GetUpdateContactDto();

        var result = await _phonebookService.UpdateContactAsync(updatedContact);

        Assert.Equal(expectedResult, result);
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateContactAsync_Returns_False_WhenFailed()
    {
        var expectedResult = false;

        _mockRepo
            .Setup(r => r.UpdateContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(expectedResult);

        var updatedContact = Resources.GetUpdateContactDto();

        var result = await _phonebookService.UpdateContactAsync(updatedContact);

        Assert.Equal(expectedResult, result);
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteContactAsync_Returns_True_WhenSuccessful()
    {
        var expectedResult = true;

        _mockRepo
            .Setup(r => r.DeleteContactAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedResult);

        var result = await _phonebookService.DeleteContactAsync(ContactId);

        Assert.Equal(expectedResult, result);
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteContactAsync_Returns_False_WhenFailed()
    {
        var expectedResult = false;

        _mockRepo
            .Setup(r => r.DeleteContactAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedResult);

        var result = await _phonebookService.DeleteContactAsync(ContactId);

        Assert.Equal(expectedResult, result);
        Assert.False(result);
    }

    [Fact]
    public async Task GetContactAsync_Returns_RetrievedContactDto_WhenSuccessful()
    {
        var expectedResult = Resources.GetRetrievedContactDto();
        var repoResult = Resources.GetContact();

        _mockRepo
            .Setup(r => r.GetContactAsync(It.IsAny<int>()))
            .ReturnsAsync(repoResult);

        var result = await _phonebookService.GetContactAsync(ContactId);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Email, result.Email);
        Assert.Equal(expectedResult.FirstName, result.FirstName);
        Assert.Equal(expectedResult.PhoneNumbers.Count, result.PhoneNumbers.Count);
    }

    [Fact]
    public async Task GetContactAsync_Returns_Null_WhenFailed()
    {
        RetrievedContactDto? expectedResult = null;
        Contact? repoResult = null;

        _mockRepo
            .Setup(r => r.GetContactAsync(It.IsAny<int>()))
            .ReturnsAsync(repoResult);

        var result = await _phonebookService.GetContactAsync(ContactId);

        Assert.Null(result);
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public async Task GetContactsAsync_Returns_ListOfRetrievedContactDtos_WhenSuccessful()
    {
        var expectedResult = Resources.GetPagedRetrievedContactDtos();
        var repoResult = Resources.GetPagedContacts();

        _mockRepo
            .Setup(r => r.GetContactsAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(repoResult);

        var result = await _phonebookService.GetContactsAsync(Page, PageSize);

        Assert.NotEmpty(result);
        Assert.Equal(expectedResult.Count, result.Count);
        Assert.Equal(expectedResult.GetType(), result.GetType());
        Assert.Equal(expectedResult[1].FirstName, result[1].FirstName);
        Assert.Equal(expectedResult[0].PhoneNumbers.Count, result[0].PhoneNumbers.Count);
        Assert.Equal(expectedResult[2].DateOfBirth.HasValue, result[2].DateOfBirth.HasValue);
    }

    [Fact]
    public async Task GetContactsAsync_Returns_EmptyList_WhenFailed()
    {
        var expectedItems = new List<RetrievedContactDto>();
        var repoItems = new List<Contact>();

        var expectedCount = expectedItems.Count;
        var repoCount = repoItems.Count;

        var expectedResult = new PagedList<RetrievedContactDto>(expectedItems, expectedCount, Page, PageSize);
        var repoResult = new PagedList<Contact>(repoItems, repoCount, Page, PageSize);

        _mockRepo
            .Setup(r => r.GetContactsAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(repoResult);

        var result = await _phonebookService.GetContactsAsync(Page, PageSize);

        Assert.Empty(result);
        Assert.Equal(expectedItems.Count, result.Count);
    }

    [Fact]
    public async Task GetContactCountAsync_Returns_CountOfContacts_WhenThereAreContacts()
    {
        var expectedResult = 3;

        _mockRepo
            .Setup(r => r.GetContactCountAsync())
            .ReturnsAsync(expectedResult);

        var result = await _phonebookService.GetContactCountAsync();

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public async Task GetContactCountAsync_Returns_0_WhenThereAreNoContacts()
    {
        var expectedResult = 0;

        _mockRepo
            .Setup(r => r.GetContactCountAsync())
            .ReturnsAsync(expectedResult);

        var result = await _phonebookService.GetContactCountAsync();

        Assert.Equal(expectedResult, result);
    }
}
