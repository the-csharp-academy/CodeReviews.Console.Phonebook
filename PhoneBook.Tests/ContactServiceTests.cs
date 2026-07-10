using PhoneBook.Models;
using PhoneBook.Services;
using PhoneBook.Tests.Helpers;

namespace PhoneBook.Tests;

public class ContactServiceTests
{
    [Fact]
    public void AddAndSearchContact_PersistsAndFindsContact()
    {
        var service = new ContactService(TestDbContextFactory.CreateContextFactory());

        service.AddContact(new Contact
        {
            Name = "Alice Adams",
            PhoneNumber = "+15550101",
            EmailAddress = "alice@example.com"
        });

        var contact = Assert.Single(service.SearchContact("Alice"));
        Assert.Equal("Alice Adams", contact.Name);
        Assert.Equal("alice@example.com", contact.EmailAddress);
    }

    [Fact]
    public void UpdateContact_UpdatesStoredValues()
    {
        var service = new ContactService(TestDbContextFactory.CreateContextFactory());
        service.AddContact(new Contact { Name = "Alice", PhoneNumber = "+15550101" });
        var contact = Assert.Single(service.SearchContact("Alice"));

        contact.Name = "Alice Cooper";
        contact.EmailAddress = "alice.cooper@example.com";

        Assert.True(service.UpdateContact(contact));

        var updated = Assert.Single(service.SearchContact("Cooper"));
        Assert.Equal("alice.cooper@example.com", updated.EmailAddress);
    }

    [Fact]
    public void DeleteContact_ReturnsFalseWhenContactDoesNotExist()
    {
        var service = new ContactService(TestDbContextFactory.CreateContextFactory());

        Assert.False(service.DeleteContact(999));
    }
}
