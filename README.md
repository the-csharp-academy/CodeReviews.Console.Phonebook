
# 📞 Phonebook Console Application

A console application built with C# and .NET for managing a simple phonebook. The app allows for managing contacts and categories, and supports sending emails to contacts.

The user interface is built using the [Spectre.Console](https://spectreconsole.net/) library for a richer, interactive console experience.

## ✨ Features

* **Contact Management:**
    * List all contacts
    * Add a new contact
    * Edit an existing contact
    * Delete a contact
* **Category Management:**
    * List all categories
    * Add a new category
    * Edit an existing category
    * Delete a category
* **Communication:**
    * Send an email to a selected contact (via GMail SMTP)

## 🛠️ Technologies Used

* **.NET 9** 
* **C#**
* **Spectre.Console:** For the interactive UI components.
* **SQL Server:** For data storage.
* **Net.Mail (SMTP):** For sending emails.

## 🚀 Installation and Setup

### 1. Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download)
* An instance of SQL Server (local or remote)

### 2. Get Started

1.  **Clone the repository:**
    ```bash
    git clone [YOUR_REPOSITORY_URL]
    ```

2.  **Database Setup:**
    The `appsettings.json` file contains the base `ConnectionStrings:DefaultConnection`. Ensure the database (`PhonebookDb`) exists on your SQL Server instance (`localhost`), or update the connection string to point to your database.

### 3. Application Configuration (Important)

**Add your configuration and secrets:**
    Copy the structure below into your `appsettings.json` and fill in your personal credentials.

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=PhonebookDb;Trusted_Connection=True;TrustServerCertificate=True;"
      },
      "EmailSettings": {
        "SenderEmail": "your-email@gmail.com",
        "SenderPassword": "YOUR-16-DIGIT-GOOGLE-APP-PASSWORD"
      },
    }
    ```

**Where to get your credentials:**
* **Email (GMail):** The `SenderPassword` is **not** your account password. It is a 16-digit [Google App Password](https://myaccount.google.com/apppasswords) you must generate from your Google Account's "Security" page (2-Step Verification must be enabled)
