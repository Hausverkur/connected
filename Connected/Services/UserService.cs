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
        //Hér er skilgreint ef null er parameter í Serviceföllum þá er kallað í hinn raunverulega database
        //Þetta er gert til þess að geta notað unit test á föll í mock-Database
        private readonly IAppDataContext _db;

        public UserService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }

        //Þetta fall nær í notanda eftir Id og skilar honum tilbaka til þess að geta nálgast upplýsingar sem birtast á MyWall/UserWall
        //Kallað er í gagnagrunninn hér því ef vitnað er í _db.Users eða _db.ApplicationUsers þá kemur upp villa því miður náðist ekki að
        //laga þetta
        public ApplicationUser GetUserInfo(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var information = (from i in db.Users
                               where i.Id == userId
                               select i).FirstOrDefault();
            return information;
        }

        //Þetta fall skilar lista af notendum sem eru búnir að staðfesta vinabeiðnir notandans
        public List<ApplicationUser> GetFriends(string userId)
        {
            var friends1 = (from f in _db.Friendships
                            where f.User1.Id == userId && f.Comfirmed == true
                            select f.User2);

            var friends2 = (from f in _db.Friendships
                            where f.User2.Id == userId && f.Comfirmed == true
                            select f.User1);

            var friends = friends1.Union(friends2).ToList();

            return friends;

        }

        //Þetta fall athugar status á vináttu tveggja notanda, ef fallið skilar 2 þá eru notendur vinir
        //ef fallið skilar 1 eru annar notandinn búinn að senda vinabeiðni en hinn notandinn ekki búinn að staðfesta beiðnina
        //ef fallið skilar 0 eru þeir ekki vinir og engar vinabeiðnir hafa verið gerðar á milli notandanna
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

        //Þetta fall skilar lista af vinabeiðnum tiltekins notanda
        public List<Friendship> GetFriendRequests(string userId)
        {
            var friendships = (from requests in _db.Friendships
                                where requests.User2Id == userId
                                && requests.Comfirmed == false
                                select requests).ToList();
            return friendships;
        }

       //Þetta fall staðfestir vinbeiðni notanda og því eru þeir nú miklir vinir
        public void AcceptRequest(int friendshipId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Friendship friendship = new Friendship();
            friendship = db.Friendships.Find(friendshipId);
            friendship.Comfirmed = true;
            db.Entry(friendship).State = EntityState.Modified;
            db.SaveChanges();
        }

        //Þetta fall eyðileggur/eyðir vináttu tveggja notanda
        public void RemoveFriendship(int friendshipId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Friendship friendship = new Friendship();
            friendship = db.Friendships.Find(friendshipId);
            db.Entry(friendship).State = EntityState.Deleted;
            db.SaveChanges();
        }

        //Þetta fall finnur vinatengls tveggja notanda, þetta er hjálparfall fyrir vinabeiðnir
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