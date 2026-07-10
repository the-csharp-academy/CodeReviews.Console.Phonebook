using PhoneBook.Models;
using PhoneBook.Services;
using PhoneBook.Tests.Helpers;

namespace PhoneBook.Tests;

public class CategoryServiceTests
{
    [Fact]
    public void AddCategory_RejectsDuplicateNamesIgnoringCase()
    {
        var service = new CategoryService(TestDbContextFactory.CreateContextFactory());

        Assert.True(service.AddCategory(new Category { Name = "Family" }));
        Assert.False(service.AddCategory(new Category { Name = "family" }));
    }

    [Fact]
    public void DeleteCategory_UnassignsItsContacts()
    {
        var contextFactory = TestDbContextFactory.CreateContextFactory();
        var categoryService = new CategoryService(contextFactory);
        var contactService = new ContactService(contextFactory);
        var category = new Category { Name = "Work" };

        Assert.True(categoryService.AddCategory(category));
        contactService.AddContact(new Contact
        {
            Name = "Pat Lee",
            PhoneNumber = "+15550102",
            CategoryId = category.Id
        });
        var contact = Assert.Single(contactService.SearchContact("Pat Lee"));

        Assert.True(categoryService.DeleteCategory(category.Id));

        var updatedContact = contactService.GetContactById(contact.Id);
        Assert.NotNull(updatedContact);
        Assert.Null(updatedContact.CategoryId);
        Assert.Null(updatedContact.Category);
    }
}
