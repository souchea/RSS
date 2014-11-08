using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Database.DataContext;

namespace RSSAgregator.Database.Manager
{
    public class UserManager
    {
        public static void AddUser(User toAdd)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.Users.Add(toAdd);
                context.SaveChanges();
            }
        }

        public static void DeleteUser(User toDelete)
        {
            using (var context = new RssAgregatorDataContext())
            {
                context.Users.Remove(toDelete);
                context.SaveChanges();
            }
        }

        public static List<User> GetAllUsers()
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from user in context.Users
                        select user).ToList();
            }
        }

        public static List<User> GetUsersNumber(int number = 5)
        {
            using (var context = new RssAgregatorDataContext())
            {
                return (from user in context.Users
                        select user).Take(number).ToList();
            }
        }

        public static User GetUserById(int id)
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
