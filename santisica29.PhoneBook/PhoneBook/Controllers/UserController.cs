using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using static PhoneBook.Models.Enums;

namespace PhoneBook.Controllers;
internal class UserController
{
    internal void Add(User user)
    {
        using var db = new PhoneBookContext();
        db.Add(user);
        db.SaveChanges();
        // INSERT INTO Users (Id, Name, Email, PhoneNumber, Address)
        // VALUES (@Id, @Name, @Email, @PhoneNumber, @Address);
    }

    internal void Remove(User user)
    {
        using var db = new PhoneBookContext();
        db.Remove(user);
        db.SaveChanges();
        // DELETE FROM Users
        // WHERE Id = user.Id
    }

    internal List<User> GetAll()
    {
        using var db = new PhoneBookContext();
        List<User> list = db.Users.ToList();
        return list;
        //SELECT * FROM Users
    }

    internal User? GetUserById(int id)
    {
        using var db = new PhoneBookContext();
        User? user = db.Users.SingleOrDefault(x => x.Id == id);
        return user;

        //SELECT * FROM Users 
        //WHERE Id = id
    }

    internal void Update(User user)
    {
        using var db = new PhoneBookContext();
        db.Update(user);
        db.SaveChanges();

        //UPDATE Users
        //SET ... Values = newValues
        //WHERE Id = user.Id
    }

    internal List<User> GetByCategories(UserCategories category)
    {
        using var db = new PhoneBookContext();

        List<User> list = db.Users
            .Where(x => x.Category == category)
            .ToList();

        return list;
    }
}
