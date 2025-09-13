# Phonebook Application
- A command line based C# Application for managing phonebook contacts.
- Users are able to store names, contact numbers, telephone numbers and categories of the contact.
- The user can send an email to a selected contact.
- The user can send an sms message to a selected contact.

## Technologies Used
- C#
- SQL Server
- Entity Framework
- Spectre Console
- Twilio API (for SMS delivery)
- .Net SMPT Client Package

## Usage Steps and Details
The application allows users to Add, Update, Delete and View phonebook entries.
Users navigate the console application via the arrow keys (on the menu):

<img width="140" height="161" alt="image" src="https://github.com/user-attachments/assets/9942bddb-fb99-4f36-8df3-eacc8ef31896" />

Spectre Console is used to display entries from the database to the console:

<img width="575" height="59" alt="image" src="https://github.com/user-attachments/assets/fdac62e9-74a9-45c8-b1d1-cbdf2e919ae9" />

Users can also use the "Sent SMS" and "Sent Email" options; they are prompted to enter an ID of a contact and the relevant field in the database is used.

## Packages Used
- Entity Framework
- Spectre Console (for Console design and presentation)
- SQL Server Package
- Twilio API Package
- Configuration Manager

## Installation Steps and Requirements
All required credentials must be entered in the app.config file presented in the project - this includes:
- Local Database details (Local DB is used in SQL Server).
- Sender Address - Used for Email Integration.
- Sender Password - Used for Email Integration and authentication.
- SMPT Host - Used for Email integration - Gmail host is used by default / currently.
- Twilio SID - Used for SMS / Twilio integration.
- Twilio Auth Token - Used for SMS / Twilio integration.
- Sender phone number - used as sender phone number, used for sending SMS message.

### Local DB Installation:
This project uses local db and SQL Server for data storage - I used https://www.youtube.com/watch?v=M5DhHYQlnq8 as a resource for creating a local db instance.
The project uses Entity Framework for SQL integration; here are the steps needed for creating an instance:

1. Run ```dotnet tool install --global dotnet-ef```
2. Run ```dotnet add package Microsoft.EntityFrameworkCore.Design``` to install the package
3. Run ```dotnet ef migrations add InitialCreate``` - This creates an migrations folder to the project.
4. Run ```dotnet ef database update``` - Updates the database.

You can then connect to the database, presuming the correct database details have been added to the App.Config file.

### SMPT / Email Integration Installation and Steps:
This project allows the user to send emails to a given contact.
The user is prompted to select a contact (based on an ID); they can then send an email to the given user.

- The Email credentials are pulled from the App.Config file, specifically, the sender address, password, and SMPT host (the default being Gmail).

The mail challenge here is to ensure that your email address (sender address) is trusted to be authenticated and sent from via external apps, as this app may throw exceptions/error messages if your email account cannot feed from exteral apps.
Once your account can be authenticated to via the app, you'll be able to compose and send emails to contacts stored.

### Twilio API Integration (for SMS Integrations):
The application allows users to send SMS messages to contacts - this is done via the Twilio API.
```Please note: This application, by default, reverts to UK Phone numbers (+44 area code)```
There are 3 components used for this integration that need to be configured and added to the app.config file:
1. Twilio SID: This is pulled from your Twilio account.
2. Twilio Auth Token: This is a secret pulled from your individual Twilio account.
3. Sender Phone Number: This is the phone number that you're sending from, on the app.

Once these are added, you'll be able to send from the application via Twilio.

## Project Learnings and Considerations
- I enjoyed using Entity Framework for this application, and I definitely see the benefits of using it over raw SQL or other methods such as Dapper; I definitely look forward to using it in future projects.
- I also benefitted from using the Twilio API and integration in particular, which I found very useful to integrate here.
- The SMTP Integration here was the main challenge, due to security settings on my Gmail account, although this worked as expected once configured via my settings.

- I'd like to thank the C# Academy for the opportunity to work on this project, and I look forward to working on the next project.
