# PhoneBook.m1chael888
This is a simple console app that can be used to create and manage contacts. A contact must have a name, an email address and/or phone number can be optionally included. The app is written in C# and uses Entity Framework Core ORM for data access

# Connecting database
Before starting the app, please change the appSettingsExample.json file according to the image below. You will need to change the file name to "appSettings.json" and change the connection string to your local SQL SERVER connection string for the app to work.

<img width="739" height="98" alt="image" src="https://github.com/user-attachments/assets/7d2ffea7-82ba-4322-a49a-ad5599edda7f" />

# How it works
Upon starting the app you will be greeted by a main menu and a message prompting you to choose an option. You can add, view, update and delete contacts. There is also a button to exit the app. Navigate the menu and use the enter key to choose an option. All of the menus throughout the app work in this way.

- After selecting "Add a contact" you will prompted to enter information related to your new contact. The contact name cannot be empty, if the email field is not skipped it must follow the standard email format (x at y dot xyz), and if the phone number field is not skipped a 10 digit input must be provided. The app will not let you proceed without proper input format. If the app does not accept your input, and you would rather skip said field, you can do so by backspacing your previous attempt and skipping said field

- After selecting "View contacts" you will presented a simple list of all of your saved contacts. The application will seed some dummy contacts if the contact list is empty on startup. If you remove all the contacts from the list, selecting "View a contact" will redirect you to the main menu with a message about the contact list being empty

- After selecting "Update a contact" you will be presented with a similar list of contacts, but this time you will be able to select one (the same way youd do a menu option). After choosing which contact to edit, you will be prompted to enter new details for said contact, similarly to when you created one. All previous formatting requirements apply

- After selecting "Delete a contact" you will presented with the same list, and can simply select a contact to delete

- Choosing "Exit" will close the app

- The app will continue to loop back to the main menu after an operation finishes until you close the program
