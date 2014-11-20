using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public class UserManager : IUserManager
    {
        public void AddUser(User toAdd)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.Users.Add(toAdd);
                context.SaveChanges();
            }
        }

        public void DeleteUser(User toDelete)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.Users.Remove(toDelete);
                context.SaveChanges();
            }
        }

        public List<User> GetAllUsers()
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from user in context.Users
                        select user).ToList();
            }
        }

        public List<User> GetUsersNumber(int number = 5)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from user in context.Users
                        select user).Take(number).ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from user in context.Users
                        where user.Id == id
                        select user).SingleOrDefault();
            }
        }
    }
}
