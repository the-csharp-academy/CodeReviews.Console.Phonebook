using Phone_Book.Models;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook
{
    internal class Menu
    {
        private Contact userContact = new Contact();
        private Category userCategory = new Category();
        private ContactController contactController = new ContactController();
        private CategoryController categoryController = new CategoryController();
        private bool isRunning = true;

        public Menu()
        {
            while (isRunning)
            {
                var actionSelection = new SelectionPrompt<string>()
                   .Title("Choose action")
                   .PageSize(10)
                   .AddChoices<string>("View contacts", "Create contact", "Delete contact", "Update contact",
                   "View categories", "Create category", "Delete category", "Update category", "Exit");

                string selectedAction = AnsiConsole.Prompt(actionSelection);

                switch (selectedAction)
                {
                    case "View contacts":
                        ViewContacts();
                        break;

                    case "Create contact":
                        CreateContact();
                        break;

                    case "Delete contact":
                        DeleteContact();
                        break;

                    case "Update contact":
                        UpdateContact();
                        break;

                    case "View categories":
                        ViewCategories();
                        break;

                    case "Create category":
                        CreateCategory();
                        break;

                    case "Delete category":
                        DeleteCategory();
                        break;

                    case "Update category":
                        UpdateCategory();
                        break;

                    case "Exit":
                        Console.Clear();
                        Console.WriteLine("Goodbye");
                        isRunning = false;
                        break;
                }
            }
        }

        private List<Category> ViewCategories()
        {
            Console.Clear();
            List<Category> categoriesList = categoryController.GetAllCategories();
            if (categoriesList.Count == 0)
            {
                AnsiConsole.MarkupLine("Currently you don't have any categories\n");
            }
            else
            {
                var viewCategoriesTable = new Table()
                    .RoundedBorder()
                    .ShowRowSeparators()
                    .Title("[Blue]Categories List[/]")
                    .AddColumns("No.", "Category name");
                int number = 1;
                foreach (Category category in categoriesList)
                {
                    string stringNumber = Convert.ToString(number);
                    viewCategoriesTable.AddRow(stringNumber, category.Name);
                    number++;
                }
                AnsiConsole.Write(viewCategoriesTable);
            }
            return categoriesList;
        }

        private void CreateCategory()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("Creating new [Blue]category[/]");
            string categoryName = AnsiConsole.Ask<string>("Please type [DarkRed_1]name for new category[/]");
            userCategory.Name = categoryName;

            categoryController.CreateCategory(userCategory);
        }

        private void DeleteCategory()
        {
            Console.Clear();
            List<Category> categoryList = ViewCategories();
            if (categoryList.Count == 0)
            {
                return;
            }
            int deleteNo = 0;
            int id = 0;
            try
            {
                deleteNo = AnsiConsole.Ask<int>("Please type number of [Blue]category[/] that you want to [DarkRed_1]delete[/]");
                int chosenNo = deleteNo - 1;
                id = categoryList[chosenNo].Id;
                userCategory.Id = id;
                categoryController.DeleteCategory(userCategory);
            }
            catch (ArgumentOutOfRangeException)
            {
                AnsiConsole.MarkupLine($"There is no category with such [DarkRed_1]number, {deleteNo}[/]");
                return;
            }
        }

        private void UpdateCategory()
        {
            Console.Clear();
            List<Category> categories = ViewCategories();
            if (categories.Count == 0)
            {
                return;
            }
            int updateNo = 0;
            int id = 0;
            try
            {
                updateNo = AnsiConsole.Ask<int>("Please type number of [Blue]category[/] that you want to [DarkRed_1]update[/]");
                int chosenNo = updateNo - 1;
                id = categories[chosenNo].Id;
            }
            catch (ArgumentOutOfRangeException)
            {
                AnsiConsole.MarkupLine($"There is no category with such [DarkRed_1]number, {updateNo}[/]");
                return;
            }
            string newCategoryName = AnsiConsole.Ask<string>("Please type new [DarkRed_1]name for category[/]");
            userCategory.Name = newCategoryName;
            userCategory.Id = id;
            categoryController.UpdateCategory(userCategory);
        }

        private List<Contact> ViewContacts()
        {
            Console.Clear();

            List<Contact> contactsList = contactController.GetAllContacts();
            if (contactsList.Count == 0)
            {
                AnsiConsole.MarkupLine("Currently you don't have any contacts\n");
            }
            else
            {
                var viewContactsTable = new Table()
                    .RoundedBorder()
                    .ShowRowSeparators()
                    .Title("[Blue]Contacts List[/]")
                    .AddColumns("Category", "No.", "Contact name", "Email", "Phone number");
                int number = 1;
                foreach (Contact contact in contactsList)
                {
                    string stringNumber = Convert.ToString(number);
                    viewContactsTable.AddRow(contact.Category.Name, stringNumber, contact.ContactName, contact.Email, contact.PhoneNumber);
                    number++;
                }
                AnsiConsole.Write(viewContactsTable);
            }

            return contactsList;
        }

        private void CreateContact()
        {
            Console.Clear();

            List<Category> categories = ViewCategories();

            if (categories.Count == 0)
            {
                AnsiConsole.MarkupLine("[Red]Error![/] You have to create any [Blue]category[/], before making [Blue]contact[/]");
                return;
            }
            var categorySelect = new SelectionPrompt<string>()
                        .Title("Choose [Blue]category[/] to insert new contact")
                        .PageSize(6);

            foreach (Category category in categories)
            {
                categorySelect.AddChoice(category.Name);
            }
            string selectedCategory = AnsiConsole.Prompt(categorySelect);

            var categoryFromList = categories.FirstOrDefault(category => category.Name == selectedCategory);

            int categoryId = categoryFromList.Id;

            AnsiConsole.MarkupLine("Creating new [Blue]contact[/]");
            string contactName = AnsiConsole.Ask<string>("Please type [DarkRed_1]name for new contact[/]");
            string phoneNumber = AnsiConsole.Ask<string>("Please type [DarkRed_1]phone number for new contact[/]");
            bool isPhoneCorrect = MenuValidation.PhoneValidation(phoneNumber);
            while (!isPhoneCorrect)
            {
                phoneNumber = AnsiConsole.Ask<string>("Please type [DarkRed_1]phone number for new contact[/]");
                isPhoneCorrect = MenuValidation.PhoneValidation(phoneNumber);
            }
            string email = AnsiConsole.Ask<string>("Please type [DarkRed_1]email for new contact[/](eg. name@gmail.com)");
            bool isEmailCorrect = MenuValidation.EmailValidation(email);
            while (!isEmailCorrect)
            {
                email = AnsiConsole.Ask<string>("Please type [DarkRed_1]email for new contact[/]");
                isEmailCorrect = MenuValidation.EmailValidation(email);
            }
            userContact.CategoryId = categoryId;
            userContact.ContactName = contactName;
            userContact.PhoneNumber = phoneNumber;
            userContact.Email = email;
            contactController.CreateContact(userContact);
        }

        private void DeleteContact()
        {
            List<Contact> contactsList = ViewContacts();
            if (contactsList.Count == 0)
            {
                return;
            }
            int deleteNo = 0;
            try
            {
                deleteNo = AnsiConsole.Ask<int>("Please type number of [Blue]contact[/] that you want to [DarkRed_1]delete[/]");
                int chosenNo = deleteNo - 1;
                int id = contactsList[chosenNo].Id;
                userContact.Id = id;
                contactController.DeleteContact(userContact);
            }
            catch (ArgumentOutOfRangeException)
            {
                AnsiConsole.MarkupLine($"There is no contact with such [DarkRed_1]number, {deleteNo}[/]");
            }
        }

        private void UpdateContact()
        {
            List<Category> categoriesList = categoryController.GetAllCategories();

            var categorySelect = new SelectionPrompt<string>()
                .Title("Choose [Blue]category[/]")
                .PageSize(6);
            foreach (Category category in categoriesList)
            {
                categorySelect.AddChoice(category.Name);
            }
            var selectedCategory = AnsiConsole.Prompt(categorySelect);

            var categoryFromList = categoriesList.FirstOrDefault(category => category.Name == selectedCategory);

            int categoryId = categoryFromList.Id;

            List<Contact> contactsList = contactController.GetAllContacts();

            contactsList = contactsList
                .Where(contact => contact.CategoryId == categoryId)
                .ToList();

            if (contactsList.Count == 0)
            {
                AnsiConsole.MarkupLine("[Red]Error![/] You don't have any contacts in this category");
                return;
            }
            int id = 0;
            var contactSelect = new SelectionPrompt<string>()
                .Title("Choose [Blue]contact[/]")
                .PageSize(6);
            foreach (Contact contact in contactsList)
            {
                contactSelect.AddChoice(contact.ContactName);
            }
            string selectedContact = AnsiConsole.Prompt(contactSelect);

            var contactFromList = contactsList.FirstOrDefault(contact => contact.ContactName == selectedContact);

            id = contactFromList.Id;

            Console.Clear();
            string newContactName = AnsiConsole.Ask<string>("Please type new [DarkRed_1]name for contact[/]");
            string newPhoneNumber = AnsiConsole.Ask<string>("Please type new [DarkRed_1]phone number for contact[/]");
            bool isPhoneCorrect = MenuValidation.PhoneValidation(newPhoneNumber);
            while (!isPhoneCorrect)
            {
                newPhoneNumber = AnsiConsole.Ask<string>("Please type [DarkRed_1]phone number for new contact[/]");
                isPhoneCorrect = MenuValidation.PhoneValidation(newPhoneNumber);
            }
            string newEmail = AnsiConsole.Ask<string>("Please type new [DarkRed_1]email for contact[/]");
            bool isEmailCorrect = MenuValidation.EmailValidation(newEmail);
            while (!isEmailCorrect)
            {
                newEmail = AnsiConsole.Ask<string>("Please type [DarkRed_1]email for new contact[/]");
                isEmailCorrect = MenuValidation.EmailValidation(newEmail);
            }
            userContact.Id = id;
            userContact.ContactName = newContactName;
            userContact.PhoneNumber = newPhoneNumber;
            userContact.Email = newEmail;
            contactController.UpdateContact(userContact);
        }
    }
}