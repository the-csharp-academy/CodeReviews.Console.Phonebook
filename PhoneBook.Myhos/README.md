# Phonebook

My third intermediate console project on C# Academy.  
A console application for managing contacts info using EF with SQLserver to create and manage database queries.

# Configuration

Before running the application, you need to configure **SQL Server** and, optionally, **Mailtrap** if you want to enable email notifications.


## 2. Create the configuration file

Create a file named **appsettings.json** in the project root (or copy `appsettings.example.json` if it is included in the repository).

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=PhoneBookDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },

  "MailSettings": {
    "Host": "",
    "Port": 2525,
    "Username": "",
    "Password": "",
    "From": ""
  }
}
```

---

## 3. Configure SQL Server

Update the `DefaultConnection` value with your SQL Server instance.

### Example (Windows Authentication)

```text
Server=localhost;
Database=PhoneBookDb;
Trusted_Connection=True;
TrustServerCertificate=True;
```

### Example (SQL Authentication)

```text
Server=localhost;
Database=PhoneBookDb;
User Id=sa;
Password=YourPassword;
TrustServerCertificate=True;
```

After configuring the connection string, apply the Entity Framework Core migrations:

```bash
dotnet ef database update
```

This command will automatically create the database and its tables.

---

## 4. Configure Mailtrap (Optional)

Email functionality is optional. If the `MailSettings` section is left empty, the application will continue to work normally, but emails will not be sent.

To enable email notifications:

1. Create a Mailtrap account.
2. Create an Email Sending inbox.
3. Copy the SMTP credentials.
4. Replace the values in the `MailSettings` section.

Example:

```json
"MailSettings": {
  "Host": "sandbox.smtp.mailtrap.io",
  "Port": 2525,
  "Username": "your_username",
  "Password": "your_password",
  "From": "your_email@example.com"
}
```

---

If the email service is not configured, PhoneBook will display a notification indicating that email delivery has been skipped. All other application features will continue to work normally.


# Requeriments

- [x] This is an application where you should record contacts with their phone numbers.

- [x] Users should be able to Add, Delete, Update and Read from a database, using the console.

- [x] You need to use Entity Framework. ADO.NET, Dapper and any other ORM aren't allowed.

- [x] Your code should contain a base Contact class with at least name, email and phone number properties.

- [x] You should validate e-mails and phone numbers and let the user know what formats are expected.

- [x] Make sure you handle errors so the app doesn't crash unexpectedly in case EF or the database have problems.

- [x] You should use Code-First Approach, which means EF will create the database schema for you.

- [x] You should seed data using Entity Framework so the user has some contacts to start with.

# C# Academy Challenges

- [x] Create a functionality that allows users to add the contact's e-mail address and send an e-mail message from the app.
- [x] Expand the app by creating categories of contacts (i.e. Family, Friends, Work, etc).
- [x] Create a unit tests project and test your validation methods. Pass several invalid and valid inputs to make sure your validation works as expected.

# Features

- The application uses EF for the creation and management of queries to the database.
- The application uses SQL Server to store data.
- The app uses Mailtrap to test email sending through it using the sandbox option. If Mailtrap is not configured, the app continues to function without the ability to send emails.
- The application has unit tests to verify that the methods for validating user data entry are functioning correctly.

**Spectre Console UI**

  - The application uses a Spectre Console UI where we can navigate using the keyboard.
	
<p align="center">
  <img width="272" height="135" alt="image" src="https://github.com/user-attachments/assets/80d5f613-2154-475d-99a6-ac2be09f055d" />
</p>

- The first menu shows us three options:
    - ManageContacts
    - ManagaCategories
    - Exit (Close the program)

## ManageContacts

- The ManageContacts's menu show us a basic CRUD operations to manage the contact information we have in the database.

<p align="center">
  <img width="237" height="227" alt="image" src="https://github.com/user-attachments/assets/7998f4c4-9b50-41da-ab17-d1df4a2c8754" />
</p>

- Also, when we create a new contact, if we set up Mailtrap, it sends us an email notifying us that the user has been created in the app.

<p align="center">
  <img width="1022" height="142" alt="image" src="https://github.com/user-attachments/assets/95bf14e7-8e90-4fbb-9f99-04383e768a8c" />
</p>

- Notification in the mailtrap of the contact creation

<p align="center">
    <img width="787" height="577" alt="image" src="https://github.com/user-attachments/assets/58f66c24-00fe-4bf1-a5b4-35e9d1259daf" />
</p>

- If it is not configured, a message will appear telling us that it is not available.

<p align="center">
  <img width="1027" height="148" alt="image" src="https://github.com/user-attachments/assets/4aa37c66-d3fd-48a2-8fd4-f443f8afef89" />
</p>

- To view a contact, the following menu appears, where we select the contact and can then view their full information

<p align="center">
  <img width="221" height="292" alt="image" src="https://github.com/user-attachments/assets/4620f5f3-2d43-452d-9403-e078764eee0c" />
  <img width="1068" height="377" alt="image" src="https://github.com/user-attachments/assets/7c0c0e98-7299-4dd4-981d-6602edda9ec7" />
</p>

- When we look at all the contacts, we see the following:

<p align="center">
  <img width="1457" height="376" alt="image" src="https://github.com/user-attachments/assets/d4ef4c13-3c0a-4319-bd0a-3b3ff422d2f3" />
</p>

- To update a contact, select a contact and then update the information you want.

<p align="center">
  <img width="243" height="306" alt="image" src="https://github.com/user-attachments/assets/f5303d04-9df1-4c33-9b30-8251caec3e64" />
  <img width="407" height="191" alt="image" src="https://github.com/user-attachments/assets/8a8a13ac-4b7e-49b8-be8b-c7804bbe59ed" />
  <img width="513" height="347" alt="image" src="https://github.com/user-attachments/assets/58ef7da4-c8d7-438b-a3e5-30742dffef3d" />
</p>

- To delete a contact, select it from the list provided, and it will be deleted from the database.

<p align="center">
  <img width="235" height="311" alt="image" src="https://github.com/user-attachments/assets/ebff206c-0f9d-4706-833d-b3f2286b5be4" />
  <img width="1453" height="340" alt="image" src="https://github.com/user-attachments/assets/08c7b8c1-d29c-40df-ae96-17b03b0d1f96" />
</p>

- As the name suggests, the "Send an Email" option allows us to send an email to a contact saved in the app.

  <p align="center">
    <img width="452" height="303" alt="image" src="https://github.com/user-attachments/assets/99e20b5d-3c01-4d59-9fa2-e67fe0550622" />
    <img width="688" height="125" alt="image" src="https://github.com/user-attachments/assets/b96c6173-0c79-4b34-add1-5efaf288b27e" />
    <img width="967" height="813" alt="image" src="https://github.com/user-attachments/assets/f33f19fd-157a-4c6d-b6bb-086cddf88528" />
  </p>

- If Mailtrap is not configured, it will display the following notification.

<p align="center">
  <img width="677" height="78" alt="image" src="https://github.com/user-attachments/assets/4f0ce880-afd8-4074-9f5c-6a8995d59d51" />
</p>

## ManagedCategories

For the categories, we have a basic CRUD menu with the same features as the contacts menu.

# Challenges
- Display beverage information correctly using AnsiConsole tables.
- Set up Mailtrap to send emails and verify that it works correctly.
- Create email templates and properly structure the relevant information.
- Properly create unit tests for the methods that validate user data input.

# What I have learned
- How to use EF to manage a database without having to create a repository and write SQL queries in the application.
- Configure Mailtrap to send emails, even if using the sandbox option.
- Organize the classes into different directories.
- Create a menu so that the user can select the contact or category they want to edit, instead of asking them to enter the ID of that item.

# Areas to improve
- Further explore the options EF provides for managing information in databases.
- Make better use of the tools provided by SpectreConsole to improve the console interface.

# Resourced Used 

- Microsoft documentation for List: https://learn.microsoft.com/es-es/dotnet/api/system.collections.generic.list-1?view=net-8.0
- Specter Console Documentation: https://spectreconsole.net/
- C# Academy Video about EF: https://www.youtube.com/watch?v=tDiJdthMs1Q&list=PL4G0MUH8YWiDcv8EUWTbDxDlkSndfh-T0
- Documentation on Net.Mail: https://learn.microsoft.com/es-es/dotnet/api/system.net.mail?view=net-8.0
