# Phone Book Application

## Introduction
This is a simple console-based Phone Book application implemented in C# using Entity Framework as the ORM (Object-Relational Mapper). The application allows users to manage their contacts by performing CRUD (Create, Read, Update, Delete) operations through the console. The database schema is generated using the Code-First approach in Entity Framework, and SQL Server is used as the database provider.

## Features
- Add, update, delete, and view contacts
- Categorize contacts into different groups (e.g., Family, Friends, Work, etc.)
- Validate email and phone number formats
- Display contact details in a user-friendly console interface
- Use Entity Framework to interact with the database

## Technologies Used
- C#
- .NET Core
- Entity Framework Core
- SQL Server
- Spectre.Console (for enhanced console UI)

## Installation and Setup

### Prerequisites
Ensure you have the following installed:
- .NET SDK (latest version recommended)
- SQL Server (LocalDB is supported)

### Clone the Repository
```sh
git clone <repository-url>
cd phonebook-app
```

### Configure the Database
Modify the connection string in `PhonebookContext.cs` if necessary:
```csharp
optionsBuilder.UseSqlServer("Server=(LocalDb)\mssqllocaldb;Database=Phonebook;Trusted_Connection=True;");
```
Run database migrations:
```sh
dotnet ef migrations add InitialMigration
dotnet ef database update
```

### Build and Run the Application
```sh
dotnet build
dotnet run
```

## Usage

### Main Menu
Upon running the application, you will be presented with the main menu:
```
What would you like to do?
1. Manage Contacts
2. Manage Categories
3. Quit
```

### Managing Contacts
- **Add Contact**: Enter the contact details including name, phone number (E.164 format), and email.
- **Update Contact**: Modify existing contact details.
- **Delete Contact**: Remove a contact from the database.
- **View Contacts**: Display all stored contacts.

### Managing Categories
- **Add Category**: Create a new category for contacts.
- **Update Category**: Rename an existing category.
- **Delete Category**: Remove a category (and optionally its associated contacts).
- **View Categories**: Display all categories.

## Validation Rules
- **Email**: Must follow the format `local@domain.tld`.
- **Phone Number**: Must follow E.164 format (e.g., `+14151231234`).
- **Name**: Must contain only letters, spaces, and `/`.
