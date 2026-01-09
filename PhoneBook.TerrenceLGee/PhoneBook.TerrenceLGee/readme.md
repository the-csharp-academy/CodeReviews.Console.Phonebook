# Phonebook App

Console application written with C# 14/.NET 10 using Microsoft's Visual Studio Community 2026 on Windows 11.

This is a console based application that allows you to add contacts to a 'phonebook'.

Created following the curriculm of [C# Academy](https://www.thecsharpacademy.com/)

[Phonebook](https://www.thecsharpacademy.com/project/16/phonebook)

## Features
- Allows the end user to add, update, delete, and view contacts from their 'phonebook'.
- Also allows the user to send an email to a contact - if the contact has a valid email address.
- Stores contact information in a Microsoft SQL Server database for persistance and easy retrieval.
- Unit tests are include to test the service layer of this application.

## Challenges Faced When Implementing This Project
- Learning how to go from using a 'micro-ORM' like Dapper to using a 'true ORM' like Entity Framework Core. Not so much a challenge, but more just getting used to using this library.
- The biggest challenge by far was learning how to unit test the email service in this application.

## What I Learned Implementing This Project
- Entity Framework Core. Learning how to use Entity Framework Core's Fluent API. Also using Entity Framework Core's LINQ Queries instead of the raw SQL written in previous projects that used Dapper.
- What I believe is a more efficient use of pagination compared to my previous projects. Instead of calliing everything from the database at once time which depending on the number of rows/items in your database can  quickly overwhelm your application, only querying the a certain amout of items from the database at a time.
- The importance of validation. Have classes/methods to validate valid input is particularly important when dealing with email address and telephone numbers. Using regular expressions and external libraries helped tremendously in this endeavor.
- How to use gmail to send email from a C#/.NET application. To be honest thanks to the external library Mailkit this was really easy to implement.

## Instructions To Create The Database And Run The Application
- You will have to go to appsettings.json (please create this file if it does not exist) and enter your Microsoft SQL Server Connection string as follows:

```
{
  "ConnectionStrings": {
    "PhonebookDb": "Server=(localdb)\\MSSQLLocalDB;Database=PhonebookDb;Trusted_Connection=True;"
  }
}
```
- If you wish to change the connection string name of 'PhonebookDb' to something else, you must also go into Program.cs and change the name in the initial configuration:

```
    var services = new ServiceCollection()
        .AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true))
        .AddDbContextFactory<PhonebookContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("PhonebookDb") ?? throw new
        InvalidOperationException("Connection string 'PhonebookDb' not found.")))
```
- You can start the application after you have added/updated your connection string information. On Visual Studio and JetBrains Rider you can simply build and run the program from the IDE.
- With Visual Studio Code you can enter the following .NET CLI commands from the command line of your choice from the project directory:

```
dotnet build && dotnet run
```

## Instructions To Send Email From This Application
- First off you will need a gmail account. Once inside of your gmail account click on the Google Account icon.
- Click on the button that says 'Manage your Google Account'.
- You will be taken to your Account options. Click on the option 'Security & sign-in' Click on and enable Two-Factor Authentication if it is not already enabled.
- After you have enabled Two-Factor Authentication, go to search at the top of the page and type in 'App Password'
- Click on 'App Password' in the results that are displayed from the drop down. From there you will be taken to a page to generate an app password.
- Simply type in the name for the App Password and click create and from there a pop up will display with your app password. Please copy this password so that you will have it for the next step.
- Next you need to set up the configuration files with the relevant information that will allow email to be sent from this application.
- In the email service method you will find the following lines that extract information from the configuration files:

```
 var senderEmail =   _configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
 var smtpPassword =  _configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
 var smtpHost =      _configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
 var portString =    _configuration.GetValue<string>("EMAIL_CONFIGURATION:PORT");
```
- Now please go to appsettings.json. This is where the relevant Smtp host and port information will need to be stored:
```
{
  "ConnectionStrings": {
    "PhonebookDb": "Server=(localdb)\\MSSQLLocalDB;Database=PhonebookDb;Trusted_Connection=True;"
  },
  "EMAIL_CONFIGURATION": {
    "HOST": "smtp.gmail.com",
    "PORT": "587"
  }
}
```
- For security purposes sensitive information such as passwords should not be stored in the application's source code/project. So the next thing to do is to configure usage of 'Secrets'.
- If you are using Visual Studio: In the solution explorer right click on your project name and scroll down until you see the option 'Manage User Secrets' click on that option and a secrets.json file will open up.
- If you are using Jetbrains Rider: From the 'solution explorer' of Rider right click on your project name and then scroll down until you see the 'Tools' option and click on that. From there another dialog opens up and then just click on the option '.NET User Secrets' and a secrets.json file will open up.
- Instructions for configuring usage of 'User Secrets' for Visual Studio Code will be after these instructions
- Visual Studio/Jetbrains Rider users simply enter your Gmail email address and your app password in the secrets.json file as follows:
```
{
  "EMAIL_CONFIGURATION": {
    "EMAIL": "your_email_address@gmail.com",
    "PASSWORD": "your app password"
  }
}
```
- Visual Studio Code users have two options. The first is to add the extension 'Manage User Secrets' Then go to the file/solution explorer of Visual Studio Code in your project and right-click on the .csproj file. Then simply click on the 'Manage User Secrets' options from the context menu which will open up the secrets.json file for editing. Simply follow the above template to add your email address and app password to the application.
- The second option is to use the .NET CLI from the project directory to add your user secrets:
```
dotnet user-secrets set "EMAIL_CONFIGURATION:EMAIL" "your_email_address@gmail.com"
dotnet user-secrets set "EMAIL_CONFIGURATION:PASSWORD" "your app password"
```

- Now in the project .csproj file you will find the following located in the property group section:
```
<UserSecretsId>xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx</UserSecretsId>
```
- You will need the UserSecretsId in order for the application to find and use the configuration that you have set in secrets.json
- In Program.cs there will be a method called Startup(). At the top of that method that will be configuration setup:
```
IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
        .AddUserSecrets("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")
        .Build();
```
- You must add your own user secrets in the chained method 'AddUserSecrets'
- After building the program you should be able to now email any contact that has an email address that is in a valid email format.

## Areas To Improve Upon
- Continue to learn and get comfortable with Entity Framework Core.
- Continue to learn more about database relationships.
- Continue to learn more and improve in writing unit tests.
- Want to get better at DRY (Do Not Repeat Yourself).
- Want to improve in all aspects of development in general.


## Technolgies Used
- [Spectre.Console](https://spectreconsole.net/)
- [Serilog](https://serilog.net/)
- [XUnit](https://xunit.net/?tabs=cs)
- [Moq](https://github.com/devlooped/moq)
- [libphonenumber-csharp](https://github.com/caseykramer/libphonenumber-csharp)
- [MailKit](https://github.com/jstedfast/MailKit)
- [Microsoft.EntityFrameworkCore](https://learn.microsoft.com/en-us/ef/)

## Helpful Resources Used
- [Initializing DbContext](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/)
- [Easy Pagination for Entity Framework in 3 steps](https://steven-giesel.com/blogPost/09285b33-79e6-4879-95e0-35aeae5fbcc6)
- [Sending emails using Gmail in C#](https://mailtrap.io/blog/csharp-send-email-gmail/)
- [Email Management With .NET and MailKit](https://dev.to/adrianbailador/email-management-with-net-9-and-c-using-mailkit-cjf)
- [Simplify and Amplify: Mocking IConfiguration in .NET the Correct Way](https://ivangechev.com/blog/unit-testing/mocking-iconfiguration)
- [C# How to mock an unmockable class](https://riccardo-santoro.medium.com/c-how-to-mock-an-unmockable-class-9f4174112c41)
- [Send email in C#: All You Need to Know](https://unione.io/en/blog/send-email-in-c-sharp)
- [Programming with Felipe Gavilan Sending Emails In ASP.NET With Gmail](https://www.youtube.com/watch?v=JX2bjkxnxTo&list=FL8cNANBvq4qCWiqtbFfaoTA&index=4)