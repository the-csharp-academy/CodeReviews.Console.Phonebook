# Requierements of Phonebook App

## Main purpose

This is an application where you can record contacts with their phone numbers.

### Operations available

Users should be able to Add, Delete, Update and Read from a database, using the console.

### Dependencies

You need to use Entity Framework, raw SQL isn't allowed.

### Models

Your code should contain a base Contact class with AT LEAST {Id INT, Name STRING, Email STRING and Phone Number(STRING)}

### Validation

You should validate e-mails and phone numbers and let the user know what formats are expected

### Approach

You should use Code-First Approach, which means EF will create the database schema for you.

### SQL Platform

You should use SQL Server, not SQLite

### Create user defined Categories of Contacts

You should create a Categories class with {Id INT, Name STRING} and use it for the Contacts structure. Contact should have a navigation property to Categories.