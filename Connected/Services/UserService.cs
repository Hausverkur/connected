using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;
using Connected.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Connected.Services
{
    public class UserService
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUser GetUserInfo(string userId)
        {
            var information = (from i in db.Users
<<<<<<< HEAD
                               where i.Id == userId
                               select i).FirstOrDefault();
=======
                where i.Id == userId
                select i).First();
>>>>>>> 738c4e073e76330722ad2d3438d82fa1895efed4

            return information;
        }

        public List<ApplicationUser> GetFriends(string userId)
        {
            var friends1 = (from f in db.Friendships
                where f.User1.Id == userId
                select f.User2);

            var friends2 = (from f in db.Friendships
                where f.User2.Id == userId
                select f.User1);

            var friends = friends1.Union(friends2).ToList();

            return friends;

        }

<<<<<<< HEAD
        public int AreFriends(string userId, string friendId)
        {
            var friends1 = (from f in db.Friendships
                            where f.User1.Id == userId
                                  && f.User2.Id == friendId
                            select f.Comfirmed);
            var friends2 = (from f in db.Friendships
                where f.User1.Id == friendId
                      && f.User2.Id == userId
                select f.Comfirmed);

            var friends = friends1.Union(friends2).FirstOrDefault();
            if (friends != null && friends == true) return 2;
            else if (friends != null && friends == false) return 1;
            else return 0;
        }
=======
        /*public List<ApplicationUser> GetAllUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var user = (from u in db.Users
                select u).ToList();

            return user;

        }*/
>>>>>>> 738c4e073e76330722ad2d3438d82fa1895efed4
    }
}