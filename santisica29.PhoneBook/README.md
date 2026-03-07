# PhoneBook
Console app that allows you to keep your contact's info in a neat personal phone book.

Add users with it's contact info (name, email, phone number, address), separated them by categories and even sent them an email with your gmail account.

Build with C#/.NET, EF Core, SQL Server, Spectre.Console, Mailkit for the email service and nUnit framework for the unit testing.

# Given Requirements
- This is an application where you should record contacts with their phone numbers.

- Users should be able to Add, Delete, Update and Read from a database, using the console.

- You need to use Entity Framework. ADO.NET, Dapper and any other ORM aren't allowed.

- Your code should contain a base Contact class with at least name, email and phone number properties.

- You should validate e-mails and phone numbers and let the user know what formats are expected.

- Make sure you handle errors so the app doesn't crash unexpectedly in case EF or the database have problems.

- You should use Code-First Approach, which means EF will create the database schema for you.

- You should seed data using Entity Framework so the user has some contacts to start with.

# Features 
- SQL Server database connection
- EF Core Code-First Approach, first make the model and then use migrations with Package Manager Console to create the database.
- Console based UI powered with Spectre.Console
- CRUD DB Functions, Add, Delete, Update and Get Users.
- Filter Users by categories.
- Send an email with your gmail account to any user in your phone's book app (You need a gmail account).
- Unit tests for validation methods using nUnit framework.

# Challenges

- Using EF Core for the first time and learning it syntaxis.
- Figuring out how to send an email through a C# console app, Mailkit made this surprisingly easy to do.
- Making unit tests for different validation methods.
- Learning about migrations, dbContext and package manager console.
- Parsing enums to string in the migration to the database.

# Own Thoughts About This Project

Overall it was a good experience, I ended up using different frameworks, approaches to coding for the first time.
Since the app was simple I could focus on learning this new technologies without worrying too much about the app itself.
It had it's challenges but it was very rewarding.

# How to Run 

- You need to have Sql server installed, with local db and you have to use the default name MSSQLLocalDB so the db connection can work successfully.

- When you try to send an email, you need to have a gmail account and a 16 characters code that gmail generates to do it. [Visit this site to do it](https://myaccount.google.com/apppasswords).

# Resources
- [Project idea from The C# Sharp Academy](https://www.thecsharpacademy.com/project/16/phonebook)
- [EF Core Microsoft documentation](https://learn.microsoft.com/en-us/ef/core/)
- [EF Core tutorial](https://www.youtube.com/watch?v=tDiJdthMs1Q&list=PL4G0MUH8YWiDcv8EUWTbDxDlkSndfh-T0)
- [Mail kit package](https://github.com/jstedfast/MailKit)
- And lots of google of course...