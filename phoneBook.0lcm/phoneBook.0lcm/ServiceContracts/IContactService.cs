using phoneBook._0lcm.Models;

namespace phoneBook._0lcm.ServiceContracts;

public interface IContactService
{
    public Task SaveContactInfo(ContactClass contactClass);
    public List<ContactClass> GetContactClasses();
    public Task EditContactClass(ContactClass newContactClass, ContactClass oldContactClass);
    public Task DeleteContactClass(ContactClass contactClass);
}