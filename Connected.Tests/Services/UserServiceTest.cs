using System;
using System.Security.Cryptography;
using System.Threading;
using Connected.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Connected.Tests;
using Connected.Models;

namespace Connected.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService _service;

        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDataContext();

            var u1 = new ApplicationUser
            {
                Id = "user1",
            };

            var u2 = new ApplicationUser
            {
                Id = "user2",
            };

            var u3 = new ApplicationUser
            {
                Id = "user3",
            };

            var u4 = new ApplicationUser
            {
                Id = "user4",
            };
            
            var f1 = new Friendship
            {
                Comfirmed = true,
                Id = 1,
                User1Id = "user1",
                User2Id = "user2",
                User1 = u1,
                User2 = u2,
            };
            mockDb.Friendships.Add(f1);
            var f2 = new Friendship
            {
                Comfirmed = true,
                Id = 2,
                User1Id = "user3",
                User2Id = "user4",
                User1 = u3,
                User2 = u4,
            };
            mockDb.Friendships.Add(f2);
            var f3 = new Friendship
            {
                Comfirmed = true,
                Id = 3,
                User1Id = "user3",
                User2Id = "user1",
                User1 = u3,
                User2 = u1,
            };
            mockDb.Friendships.Add(f3);
            var f4 = new Friendship
            {
                Comfirmed = false,
                Id = 4,
                User1Id = "user3",
                User2Id = "user2",
                User1 = u3,
                User2 = u1,
            };
            mockDb.Friendships.Add(f4);
            var f5 = new Friendship
            {
                Comfirmed = false,
                Id = 5,
                User1Id = "user1",
                User2Id = "user4",
                User1 = u3,
                User2 = u1,
            };
            mockDb.Friendships.Add(f5);
            var f6 = new Friendship
            {
                Comfirmed = false,
                Id = 6,
                User1Id = "user2",
                User2Id = "user4",
                User1 = u3,
                User2 = u1,
            };
            mockDb.Friendships.Add(f6);
            var p1 = new UserPost
            {
                Id = 1,
                Body = "Halló",
                User = u1,
                UserId = "user1",
                GroupReference = 1,
                GroupPost = false,
                DateTimePosted = DateTime.Now,
            };
            mockDb.UserPosts.Add(p1);
            var p2 = new UserPost
            {
                Id = 2,
                Body = "Halló",
                User = u1,
                UserId = "user1",
                GroupReference = 1,
                GroupPost = false,
                DateTimePosted = DateTime.Now,
            };
            mockDb.UserPosts.Add(p2);


            _service = new UserService(mockDb); 
            
        }

        [TestMethod]
        public void TestGetFriendsForUser1()
        {
            //ARRANGE:
            const string user = "user1";
            
            //ACT:
            var friends = _service.GetFriends(user);

            //ASSERT:
            Assert.AreEqual(2, friends.Count);
            foreach (var item in friends)
            {
                Assert.AreNotEqual(item, "user1");
            }
        }

        [TestMethod]
        public void TestGetFriendsForUser2()
        {
            //ARRANGE:
            const string user = "user2";

            //ACT:
            var friends = _service.GetFriends(user);

            //ASSERT:
            Assert.AreEqual(1, friends.Count);
            foreach (var item in friends)
            {
                Assert.AreNotEqual(item, "user2");
            }
        }

        [TestMethod]
        public void TestAreFriends()
        {
            //ARRANGE:
            const string user1 = "user1";
            const string user2 = "user2";

            //ACT:
            var friendship = _service.AreFriends(user1, user2);
            
            //ASSERT:
            Assert.AreEqual(2, friendship);
        }

        [TestMethod]
        public void TestAreNotFriends()
        {
            //ARRANGE:
            const string user1 = "user4";
            const string user2 = "user1";

            //ACT:
            var friendship = _service.AreFriends(user1, user2);

            //ASSERT:
            Assert.AreEqual(0, friendship);
        }

        //Næ ekki þessu til þess að virka
        [TestMethod]
        public void TestAreNotYetConfirmedFriends()
        {
            //ARRANGE:
            const string user1 = "user2";
            const string user2 = "user4";

            //ACT:
            var friendship = _service.AreFriends(user1, user2);

            //ASSERT:
            Assert.AreEqual(1, friendship);
        }

        [TestMethod]
        public void TestGetUser2FriendRequest()
        {
            //ARRANGE:
            const string user = "user2";
            
            //ACT:
            var requests = _service.GetFriendRequests(user);

            //ASSERT:
            Assert.AreEqual(1, requests.Count);
        }

        [TestMethod]
        public void TestGetUser4FriendRequest()
        {
            //ARRANGE:
            const string user = "user4";

            //ACT:
            var requests = _service.GetFriendRequests(user);

            //ASSERT:
            Assert.AreEqual(2, requests.Count);
        }

        [TestMethod]
        public void TestGetUser1FriendRequest()
        {
            //ARRANGE:
            const string user = "user1";

            //ACT:
            var requests = _service.GetFriendRequests(user);

            //ASSERT:
            Assert.AreEqual(0, requests.Count);
        }

        [TestMethod]
        public void GetFriendshipIdForUsers3And1()
        {
            //ARRANGE:
            const string user1 = "user3";
            const string user2 = "user1";

            //ACT:
            var id = _service.FindFriendship(user1, user2);

            //ASSERT:
            Assert.AreEqual(3, id);
        }

        [TestMethod]
        public void GetFriendshipIdForUsers2And4()
        {
            //ARRANGE:
            const string user1 = "user2";
            const string user2 = "user4";

            //ACT:
            var id = _service.FindFriendship(user1, user2);

            //ASSERT:
            Assert.AreEqual(6, id);
        }
    }
}
