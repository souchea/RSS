using System.Collections.Generic;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public interface IUserManager
    {
        void AddUser(User toAdd);
        void DeleteUser(User toDelete);
        List<User> GetAllUsers();
        List<User> GetUsersNumber(int number = 5);
        User GetUserById(int id);
    }
}