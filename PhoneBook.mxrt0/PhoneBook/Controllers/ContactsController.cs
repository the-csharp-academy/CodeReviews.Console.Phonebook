using PhoneBook.DBContext;
using PhoneBook.Entities;
using PhoneBook.Utils;
using PhoneBook.Utils.Enums;

namespace PhoneBook.Controllers
{
    public class ContactsController
    {
        private ContactsDbContext _dbContext;

        public ContactsController(ContactsDbContext dbContext)
        {
            _dbContext = dbContext;
            CreateDb();
            MainMenu();
        }

        public void CreateDb() => _dbContext.Database.EnsureCreated();

        private void MainMenu()
        {
            Console.Clear();

            Console.WriteLine(Messages.MainMenuMessage);
            UIHelper.DisplayOptions();

            string? userInput = Console.ReadLine();

            while (!Validator.IsUserInputValid(userInput))
            {
                Console.WriteLine(Messages.InvalidInputMessage);
                userInput = Console.ReadLine();
            }
            HandleUserInput(Enum.Parse<MenuOption>(userInput!));
        }

        private void HandleUserInput(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.ExitApp:
                    Console.WriteLine(Messages.ExitMessage);
                    Environment.Exit(0);
                    break;
                case MenuOption.AddContact:
                    AddNewContact();
                    break;
                case MenuOption.DeleteContact:
                    DeleteContact();
                    break;
                case MenuOption.UpdateContact:
                    UpdateContact();
                    break;
                case MenuOption.ViewContact:
                    ViewContact();
                    break;
                case MenuOption.AddCategory:
                    AddNewCategory();
                    break;
                case MenuOption.DeleteCategory:
                    DeleteCategory();
                    break;
                case MenuOption.UpdateCategory:
                    UpdateCategory();
                    break;
                case MenuOption.ViewCategory:
                    ViewCategory();
                    break;

            }
            MainMenu();
        }

        private void ViewCategory()
        {
            Console.Clear();
            Console.WriteLine(Messages.CategoryNameMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            var category = GetExistingContactCategoryInput();

            Console.WriteLine($"\nCategory {category.Name}:");
            Console.WriteLine(string.Join(Environment.NewLine, _dbContext.Contacts.ToList().Where(c => c.CategoryId == category.Id)));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void UpdateCategory()
        {
            Console.Clear();
            PrintCategories();

            Console.WriteLine(Messages.CategoryToUpdateNameMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            var category = GetExistingContactCategoryInput();

            Console.WriteLine(Messages.CategoryNameMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string? categoryInput = GetValidCategoryInput();

            while (_dbContext.Categories.ToList().Any(c => string.Equals(c.Name, categoryInput, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(Messages.CategoryAlreadyExistsMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                categoryInput = GetValidCategoryInput();
            }

            category.Name = categoryInput;
            _dbContext.SaveChanges();

            Console.WriteLine(Messages.SuccessfullyUpdatedCategoryMessage);
            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void DeleteCategory()
        {
            Console.Clear();
            PrintCategories();

            Console.WriteLine(Messages.CategoryToDeleteNameMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            var category = GetExistingContactCategoryInput();

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            Console.WriteLine(string.Format(Messages.SuccessfullyDeletedCategoryMessage, category.Name));
            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void AddNewCategory()
        {
            Console.Clear();

            Console.WriteLine(Messages.CategoryNameMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string? categoryInput = GetValidCategoryInput();

            while (_dbContext.Categories.ToList().Any(c => string.Equals(c.Name, categoryInput, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(Messages.CategoryAlreadyExistsMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                categoryInput = GetValidCategoryInput();
            }

            var newCategory = new ContactsCategory(categoryInput);
            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();

            Console.WriteLine(string.Format(Messages.SuccessfullyAddedCategoryMessage, categoryInput));
            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void ViewContact()
        {
            Console.Clear();

            Console.WriteLine(Messages.ContactNameMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string contactName = GetContactNameInput();
            while (!_dbContext.Contacts.ToList().Any(contact => string.Equals(contact.Name, contactName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(Messages.ContactDoesNotExistMessage);
                contactName = GetContactNameInput();
            }
            var contact = _dbContext.Contacts.ToList().First(contact => string.Equals(contact.Name, contactName, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(contact);

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void UpdateContact()
        {
            Console.Clear();
            PrintContacts();

            Console.WriteLine(Messages.UpdateContactIdMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            int contactToUpdateId = GetContactIdInput();

            while (!_dbContext.Contacts.Any(contact => contact.Id == contactToUpdateId))
            {
                Console.WriteLine(Messages.ContactDoesNotExistMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                contactToUpdateId = GetContactIdInput();
            }

            var contactToUpdate = _dbContext.Contacts.First(contact => contact.Id == contactToUpdateId);

            Console.WriteLine(Messages.ContactNameMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string? newName = GetContactNameInput();
            contactToUpdate.Name = newName;

            Console.WriteLine(Messages.ContactEmailMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string? newEmail = GetContactEmailInput();
            contactToUpdate.Email = newEmail;

            Console.WriteLine(Messages.ContactPhoneMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string? newPhoneNumber = GetContactPhoneInput();
            contactToUpdate.PhoneNumber = newPhoneNumber;

            _dbContext.SaveChanges();
            Console.WriteLine(string.Format(Messages.SuccessfullyUpdatedContactMessage, contactToUpdateId));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void DeleteContact()
        {
            Console.Clear();
            PrintContacts();

            Console.WriteLine(Messages.DeleteContactIdMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);

            int contactToDeleteId = GetContactIdInput();

            while (!_dbContext.Contacts.Any(contact => contact.Id == contactToDeleteId))
            {
                Console.WriteLine(Messages.ContactDoesNotExistMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                contactToDeleteId = GetContactIdInput();
            }

            _dbContext.Contacts.Remove(_dbContext.Contacts.First(c => c.Id == contactToDeleteId));
            _dbContext.SaveChanges();
            Console.WriteLine(string.Format(Messages.SuccessfullyDeletedContactMessage, contactToDeleteId));

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void AddNewContact()
        {
            Console.Clear();

            Console.WriteLine(Messages.ContactNameMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string? nameInput = GetContactNameInput();

            Console.WriteLine(Messages.ContactEmailMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string? emailInput = GetContactEmailInput();

            Console.WriteLine(Messages.ContactPhoneMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string? phoneInput = GetContactPhoneInput();

            Console.WriteLine(Messages.ContactCategoryMesage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            var category = GetExistingContactCategoryInput();

            var newContact = new Contact(nameInput, emailInput, phoneInput);
            newContact.SetCategory(category);

            _dbContext.Contacts.Add(newContact);
            _dbContext.SaveChanges();
            Console.WriteLine(Messages.SuccessfullyAddedContactMessage);

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private void CheckReturnToMainMenu(string? input)
        {
            if (!string.IsNullOrEmpty(input) && input == "0")
            {
                MainMenu();
            }
        }

        private int GetContactIdInput()
        {
            string? contactToDeleteIdInput = Console.ReadLine();
            CheckReturnToMainMenu(contactToDeleteIdInput);
            int contactToDeleteId = 0;
            while (!int.TryParse(contactToDeleteIdInput, out contactToDeleteId) || contactToDeleteId <= 0)
            {
                Console.WriteLine(Messages.InvalidIdMessage);
                contactToDeleteIdInput = Console.ReadLine();
            }

            return contactToDeleteId;
        }

        private string GetContactNameInput()
        {
            string? nameInput = Console.ReadLine();
            CheckReturnToMainMenu(nameInput);

            while (!Validator.IsContactNameInputValid(nameInput))
            {
                Console.WriteLine(Messages.InvalidContactNameMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                nameInput = Console.ReadLine();
                CheckReturnToMainMenu(nameInput);
            }
            return nameInput!;
        }

        private string GetContactEmailInput()
        {
            string? emailInput = Console.ReadLine();
            CheckReturnToMainMenu(emailInput);

            while (!Validator.IsContactEmailInputValid(emailInput))
            {
                Console.WriteLine(Messages.InvalidContactEmailMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                emailInput = Console.ReadLine();
                CheckReturnToMainMenu(emailInput);
            }
            return emailInput!;
        }
        private string GetContactPhoneInput()
        {
            string? phoneInput = Console.ReadLine();
            CheckReturnToMainMenu(phoneInput);

            while (!Validator.IsContactPhoneInputValid(phoneInput))
            {
                Console.WriteLine(Messages.InvalidContactPhoneMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                phoneInput = Console.ReadLine();
                CheckReturnToMainMenu(phoneInput);
            }
            return phoneInput!;
        }

        private ContactsCategory GetExistingContactCategoryInput()
        {
            string? categoryName = GetValidCategoryInput();
            CheckReturnToMainMenu(categoryName);

            while (!_dbContext.Categories.ToList().Any(c => string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(Messages.CategoryNameDoesNotExistMessage);
                categoryName = GetValidCategoryInput();
            }
            return _dbContext.Categories.ToList().First(c => string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase));
        }

        private string GetValidCategoryInput()
        {
            string? categoryInput = Console.ReadLine();
            CheckReturnToMainMenu(categoryInput);

            while (string.IsNullOrEmpty(categoryInput) || !categoryInput.Any(char.IsLetter))
            {
                Console.WriteLine(Messages.InvalidContactNameMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                categoryInput = Console.ReadLine();
            }
            return categoryInput;
        }

        private void PrintContacts()
        {
            Console.WriteLine("\nYour contacts:\n");
            Console.WriteLine(string.Join(Environment.NewLine, _dbContext.Contacts.ToList()));
        }

        private void PrintCategories()
        {
            Console.WriteLine("\nYour categories:\n");
            Console.WriteLine(string.Join(Environment.NewLine, _dbContext.Categories.ToList().Select(c => c.Name)));
        }
    }
}
