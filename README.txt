Phone book - entity framework


The flow:
Program.cs
Shows menu loop (switch on MenuOptions enum)
 
AddContact → ContactController.AddContact()
Asks for input, opens db, saves new Contact

ViewAllContacts → ContactController.ViewAllContacts()
Opens db, returns List<Contact>

ViewContact/Update/Delete

ContactService.GetContactOptionInput()
Calls ViewAllContacts() → gets all contacts
Extracts names into array with Select()
Displays names as Spectre menu
User picks name → Single() finds matching contact → extracts Id
Calls ViewContactById(id) → returns Contact object

ViewContact → displays the contact
UpdateContact → modifies and SaveChanges()
DeleteContact → removes and SaveChanges()


DbContext is EF Core's engine. It handles opening connections, tracking changes, generating SQL, executing queries.For our Context class to have these functions it needs to inherit DBContext.

ContactsContext is my version of that engine, it controls:

Where the database is (OnConfiguring to connection string)
What tables exist (DbSet<Contact> Contacts to contacts table)

Packages Used:
Spectre.Console
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Console

The GetContactOptionInput 

I first started by creating a menu using a switch for each CRUD operation and used an enumerator to generate types for each option, AddContact, DeleteContact, UpdateContact, ViewAllContacts, ViewContact. If a user chooses one of the menu options they will be taken to the ContactController class method that applies to the option they chose.

The ContactsContext class is responsible for holding the database string, this class inherits the properties of the DbContext This means ContactsContext gains all the functionality provided by DbContext, such as Managing the database connection and Querying the database. To use the Dbcontexct class I had to install the EntityFrameworkCore package which is responsible for database management, once this was installed ContactsContext could inherit from DbContext. I then created a model class named Contact.cs which is responsible for holding the fields of the records we will collect from the user.

Below the inheritance I created a method that uses Entity Framework to use a SQLite database stored in a local file named contacts.db, which will be used as the application's data source once the database is created or initialized. I then used the package manager console to add a migration, whilst the onConfiguring method is responsible for setting up the connection, migration is responsible for carrying out those instructions.

ContactContext.cs
This class is responsible for creating / configuring the database and defining which tables Entity Framework Core manages through DbSet properties.

Contact.cs
This class is the model, the model class is responsible for the data inside the database table in this case being contact.
Each contact will need to a users ID, Name, Number and Phone. 
All fields in this method are string with the exception of the ID field which is an integer.

UserInterface.cs
This is responsible for listing out all the records in our database.
Spectre.Console is used to list in data in a presentable table instead of generic tables.

ContactService.cs 	
The ContactService class is responsible for handling contact selection.

Before carrying out CRUD operations delete, update or view, the user needs to select a contact from the database. This class handles that selection logic so it doesn't have to be repeated across every method.

GetContactOptionInput first calls ViewAllContacts to retrieve all contacts from the database.

It then puts just the names into an array using LINQ's Select and displays them as an interactive menu using Spectre.Console's SelectionPrompt. 

Once the user picks a name, Single searches the contacts list for the matching contact object and extracts its database Id. That Id is then passed to ViewContactById which fetches the full contact record and returns it to the caller.

DeleteContact uses GetContactOptionInput to let the user select a contact, then passes that contact Object to ContactController.DeleteContact to remove it from the database.

We do this instead of asking a user to enter their id is because a user shouldn't need to know database Ids. 

Contact Controller.cs
This class is responsible for where the database interactions will live.
This class holds the methods that the users choice from the switch sends them to.

The addcontact method is designed to ask the user for input for the fields we need to populate which are name, email and phone number.

Inside the AddContact method, the user is prompted for a name, email and phone number. Once the input is collected, we open a connection to the database by creating an instance of ContactsContext. We then call db.Add(), passing in a new Contact object with the users input assigned to each property using an object initialiser. Nothing is written to the database until db.SaveChanges() is called, which is what actually executes the INSERT. We populate the add function with the properties.

List<Contact>ViewAllContacts()
This method is designed to view the contacts in our database.

Inside the ViewAllContacts method we open the connection to the database by creating an instance of ContactContext. 

Then we create a variable that accesses the contacts from the database and adds them to a list we then return the variable, the point of creating this is to have a variable that returns data to the list, Calling .ToList() on it executes the SELECT query and loads all records into memory as a List<Contact>.


DeleteContact accepts a Contact object as a parameter.
The contact is passed in from GetContactOptionInput which returns the full contact the user selected, it does this by following the flow in the method which displays all contacts in the database, then asks a user to pick one, from the names in the database that name is used to look up the contact object it was given by EF and returned back to us. We open the database, call db.Remove() passing the contact object in from the GetContactOptionInput, and then SaveChanges() executes the DELETE against the database.

Created DeleteContact in the ContactService class this method is responsible for collecting the names in our database using the GetContactOptionInput method, it collects the names in the database using LINQ and list them in an array which users will choose from in order to delete a contact based on the contact name. The chosen name is used to find the matching contact in the list and retrieve the Id EF already assigned it, that Id is then used to fetch the full contact object which is returned and passed into DeleteContact.

Created DeleteContact method in contact controller, this takes the contact object passed into it from the contact service class.

Updated the DeleteContact case in Program.cs to call ContactService.DeleteContact() instead of ContactController.DeleteContact() directly. The controller is only responsible for database operations,

Updated the DeleteContact case in Program.cs to call ContactService.DeleteContact instead of ContactController.DeleteContact directly. ContactService.DeleteContact is responsible for displaying the contacts to the user and returning the selected contact object. Once we have that object we pass it into ContactController.DeleteContact which carries out the deletion against the database.

















