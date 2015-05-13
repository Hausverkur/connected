﻿using System;
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
        private readonly IAppDataContext _db;

        public UserService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }

        
        public ApplicationUser GetUserInfo(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var information = (from i in db.Users
                               where i.Id == userId
                               select i).FirstOrDefault();
            return information;
        }

        public List<ApplicationUser> GetFriends(string userId)
        {
            var friends1 = (from f in _db.Friendships
                where f.User1.Id == userId
                select f.User2);

            var friends2 = (from f in _db.Friendships
                where f.User2.Id == userId
                select f.User1);

            var friends = friends1.Union(friends2).ToList();

            return friends;

        }
        public int AreFriends(string userId, string friendId)
        {
            var friends1 = (from f in _db.Friendships
                            where f.User1.Id == userId
                                  && f.User2.Id == friendId
                            select f.Comfirmed);
            var friends2 = (from f in _db.Friendships
                where f.User1.Id == friendId
                      && f.User2.Id == userId
                select f.Comfirmed);

            var friends = friends1.Union(friends2).FirstOrDefault();
            if (friends != null && friends == true) return 2;
            else if (friends != null && friends == false) return 1;
            else return 0;
        }
    }
}