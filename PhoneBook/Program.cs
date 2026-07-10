using PhoneBook.Data;
using PhoneBook.Services;
using PhoneBook.UI;
using Microsoft.EntityFrameworkCore;

using (var context = new PhoneBookContext())
{
    context.Database.Migrate();
}

var contactService = new ContactService();
var categoryService = new CategoryService();
var emailService = new EmailService();
var smsService = new SmsService();

var ui = new UserInterface(contactService, categoryService, emailService, smsService);
ui.Run();