# Phone Book
This is a learning project made under the requirements set [here](https://thecsharpacademy.com/project/16/phonebook).  
This project allows the user to record contacts, including thier name, phone, and email. These contacts are used for basic CRUD operations such as viewing, deleting, or editing a contact.  

# Features
* Spectre.Console is used for a cleaner looking console UI.  
* After adding a contact, it becomes easy to edit, delete, or view specific details.  
* You can easily email your contacts from the app itself, with just the click of a button.  
* Besides sending emails, you can also send SMS messages from the app.  
* A settings menu is accessible from the main menu, allowing the user to tweak neccesary settings.  
* User data such as email settings are stored locally.  

# Setup
The application itself just needs to be cloned and run on your machine, however to use emailing and sms features some extra setup is required. In order to use the emailing feature
you'll have to add your email address, as well as your email's app password (not your account password) to the app. This is neccesary step in order to send emails, and is easily
accesible via the settings menu, where you can add or change your login details.  
The sms messaging feature does not require any login details, but rather requires a paid API key in order to fully work. In order to get around this the app uses a test key,
meaning no sms messages are actually sent, but rather it's just to show the application's potential. However, if the user wants to use the app for real messaging there are two 
options: The user can pay for a [Textbelt](https://textbelt.com) API key and enter it via the settings menu, or they can use the free "textbelt" key which allows for 1 free message 
per day.  

# Resources Used
[.NET (10.0)](https://learn.microsoft.com/en-us/dotnet/)  
[Spectre.Console (0.54.1-alpha.0.68)](https://spectreconsole.net/cli)  
[Microsoft.EntityFrameworkCore (10.0.0)](https://learn.microsoft.com/en-us/ef/)  
[Microsoft.EntityFrameworkCore.Sqlite (10.0.0)](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/?tabs=dotnet-core-cli)  
[Microsoft.Extensions.Hosting (11.0.0-preview.1.26104.118)](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting?view=net-10.0-pp)  
[Microsoft.EntityFrameworkCore.Design (10.0.0)](https://learn.microsoft.com/en-us/ef/core/cli/services)  
[MailKit (4.15.1)](https://www.mailkit.com)  
[MimeKit (4.15.1)](https://mimekit.net)  

# Personal Thoughts
This project was suprisingly easier than I thought it would be, which does make me a little nervous that theres something I need to do that I completely missed, but for what I've
done it didn't feel very challenging. Learning how to send an email was probably the most challenging part, but it was pretty fun. Setting up the SMS messaging was pretty simple,
the worst part was that I wanted to find a better API to use to send actual text messages for free, but I ended up settling on just being able to show a test after about an hour
of searching. Overall I thought that the project was pretty easy, or at least easier than I was expecting it to be, and it felt like a smaller project than what I have been doing.
Hopefully I'm not going to have to come back later after saying this to fix some big missing requirement. I'm a little annoyed I couldn't come up with a better option for a free
messaging feature, but I'm at least satisfied with the project, and excited to move on to the next project.
