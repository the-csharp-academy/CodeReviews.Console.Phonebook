using Phone_Book.Data;
using Phone_Book.Views;

var context = new ContactContext();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

var menu = new Menu();

menu.MainMenu();
