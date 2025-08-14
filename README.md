# Phone Book
Console-based app for managing contacts. Developed with C# and SQL Server

## Given Requirements
* Users should be able to add, delete, update and view contacts from a database, using the console.
* Entity Framework should be used rather than raw SQL
* The Contact C# class must contain at least an Id , a Name, an E-Mail, and a Phone Number.
* E-mails and phone numbers must be validated and the users should know what format to use.
* Code-First Approach is to be used, letting EF create the DB schema.
* SQL Server (as opposed to SQlite) should be used.

## Features
* Console-based menu where users can choose an action.
* CRUD functionality for contacts and categories.
* Entirely EF-controlled database workflow

## Challenges
* Getting to know how to work with EF and its difference to Dapper.

## Lessons Learned
* Use EF for straightforward DB operations to avoid executing explicit queries unnecessarily.

## Areas To Improve
* Designing better and more intuitive console-based menus for improved UX.
* DRY principle

## Resources
* EF Core Docs

# Configuration Instructions
* Located in the same folder as this README is a 'Config' folder containing an example App.config file.
* Provided the user is running SQL Server on their local PC and has enabled Trust Server Certificate when connecting through SSMS, it should be sufficient to create an App.config file within the project folder (where the PhoneBook.csproj resides) and copy-paste the contents of the example config file over to it. Under different circumstances, the 'connectionString' value should be edited accordingly.


