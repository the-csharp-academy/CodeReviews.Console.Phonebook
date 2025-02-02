# Phonebook app repository

## Phonebook Console App

Created by 

- VS 2022 
- SQL Server
- Entity Framework Core 
- Spectre Console

### Main Steps that are already implemented

  1. Project initialization
  2. Create filebase and structure for this project
  3. Create Models in Models directory
  4. Write an DbContext for this application
     After some research and unsucceessful attempts,
     - I found out that Entity Framework Core in console applications is slightly different than ASP.NET Web EF implementation.
     - I was searching, digging and finally found how to create DbContextFactory!
  5. Build working configuration file and services to provide access to database.

### Main Steps that are yet to be implemented

  1. Add Category model to existing classes, connect Contacts to Categories with a Foreign Key ✓
  2. Start implementing database CRUD operations for Contacts and Categories through Service class ✓
  3. Create user interface
  4. Create validation for Phone numbers and Emails
  5. Handle Exceptions and possible Errors
  6. Handle possible null values

#### Additional things i experienced while developing this *simple* app

- This was my first experience with Entity Framework Core and SQL Server together. I was frustrated, i didnt know how to fix all the bugs when i was trying to create an migration.. I was asking myself "What am I doing wrong?" And then i got a solution, and learned a lot. The services and the migrations are a bit confusing, but once you get used to it, it becomes very simple.. Thanks for your practices, It really sheds some light into my life right now. I appreciate all the work you do with the projects and the reviews!

-DreamFXX
