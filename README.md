# Phone Book

A console application that manages a user's contact list on a locally hosted server.
## Features

- Store all of your contacts in a locally hosted SQL Server including their name, phone number, and email address.
- Easy to use and intuitive UI for creating, viewing, and making changes to your personal contact list!
- Create and sort your contacts into categories to make it easier to find their information later.



## Tech Stack

**Runtime & Framework:** .NET 8

**Database ORM:** Entity Framework

**UI:** Spectre.Console


## Lessons Learned

- This project was my first exposure to the Entity Framework and the main focus of taking it on. Though it took me a minute to really figure out what it was I was using, I finally understood why this is so widely used. Once everything is set up, EF makes creating the database, making changes to your tables, and even basic CRUD queries super painless. I ran into two big issues working with it that took time to solve as a first time user: An issue with stale data as I held onto an instance of my dbContext too long and design time versus run time operations where I actually needed a specific factory to build the database at design time. Once I got over those two hills, working with EF was relatively smooth sailing.
## Acknowledgements

 - [The C# Academy](https://www.thecsharpacademy.com/) - Amazing community that's paving the path for others to more easily learn how to use the .NET Framework and beyond.
 - [README Editor](https://readme.so/editor)
 - [Spectre Console](https://spectreconsole.net) 

