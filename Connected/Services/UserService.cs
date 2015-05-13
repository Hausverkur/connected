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
                where i.Id == userId
                select i).First();

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

        /*public List<ApplicationUser> GetAllUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var user = (from u in db.Users
                select u).ToList();

            return user;

        }*/
    }
}