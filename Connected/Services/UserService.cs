using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                            select f);

            var friends2 = (from f in _db.Friendships
                            where f.User1.Id == friendId
                                  && f.User2.Id == userId
                            select f);

            var friends = friends1.Union(friends2).FirstOrDefault();
            if (friends != null && friends.Comfirmed == true) return 2;
            else if (friends != null && friends.Comfirmed == false) return 1;
            else return 0;
        }

        public List<Friendship> GetFriendRequests(string userId)
        {
            var friendships = (from requests in _db.Friendships
                                where requests.User2Id == userId
                                && requests.Comfirmed == false
                                select requests).ToList();
            return friendships;
        }

        public void AcceptRequest(int friendshipId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Friendship friendship = new Friendship();
            friendship = db.Friendships.Find(friendshipId);
            friendship.Comfirmed = true;
            db.Entry(friendship).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveFriendship(int friendshipId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Friendship friendship = new Friendship();
            friendship = db.Friendships.Find(friendshipId);
            db.Entry(friendship).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public int FindFriendship(string user1Id, string user2Id)
        {
            var friendship = (from f in _db.Friendships
                where (f.User1Id == user1Id && f.User2Id == user2Id) ||
                      (f.User1Id == user2Id && f.User2Id == user1Id)
                select f.Id).FirstOrDefault();

            return friendship;
        }
    }
}