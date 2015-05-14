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
    public class UserServiceTests
    {
        private UserPostService _service;

        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDataContext();

            var u1 = new ApplicationUser
            {
                Id = "user1",
            };
            mockDb.ApplicationUsers.Add(u1);
            var u2 = new ApplicationUser
            {
                Id = "user2",
            };
            mockDb.ApplicationUsers.Add(u2);
            var u3 = new ApplicationUser
            {
                Id = "user3",
            };
            mockDb.ApplicationUsers.Add(u3);
            var u4 = new ApplicationUser
            {
                Id = "user4",
            };
            mockDb.ApplicationUsers.Add(u4);
           
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
            var p3 = new UserPost
            {
                Id = 3,
                Body = "Hola",
                User = u2,
                UserId = "user2",
                GroupReference = 1,
                GroupPost = false,
                DateTimePosted = DateTime.Now,
            };
            mockDb.UserPosts.Add(p3);
            var p4 = new UserPost
            {
                Id = 4,
                Body = "Guten Tag",
                User = u3,
                UserId = "user3",
                GroupReference = 1,
                GroupPost = false,
                DateTimePosted = DateTime.Now,
            };
            mockDb.UserPosts.Add(p4);
            var p5 = new UserPost
            {
                Id = 5,
                Body = "Hey",
                User = u3,
                UserId = "user3",
                GroupReference = 1,
                GroupPost = false,
                DateTimePosted = DateTime.Now,
            };
            mockDb.UserPosts.Add(p5);
            var p6 = new UserPost
            {
                Id = 6,
                Body = "Hæ",
                User = u1,
                UserId = "user1",
                GroupReference = 1,
                GroupPost = false,
                DateTimePosted = DateTime.Now,
            };
            mockDb.UserPosts.Add(p6);


            _service = new UserPostService(mockDb); 
            
        }

        [TestMethod]
        public void TestGetPostsFromUser1()
        {
            //ARRANGE:
            const string user = "user1";

            //ADD:
            var userPosts = _service.GetPostsByUserId(user);

            //ASSERT:
            Assert.AreEqual(3, userPosts.Count);
        }

        [TestMethod]
        public void TestGetPostsFromUser2()
        {
            //ARRANGE:
            const string user = "user2";

            //ADD:
            var userPosts = _service.GetPostsByUserId(user);

            //ASSERT:
            Assert.AreEqual(1, userPosts.Count);
        }

        [TestMethod]
        public void TestGetPostsFromUser4()
        {
            //ARRANGE:
            const string user = "user4";

            //ADD:
            var userPosts = _service.GetPostsByUserId(user);

            //ASSERT:
            Assert.AreEqual(0, userPosts.Count);
        }

        [TestMethod]
        public void TestGetAllPostsForAllUsers()
        {
            //ARRANGE:
            
            //ADD:
            var userPosts = _service.GetPosts();

            //ASSERT:
            Assert.AreEqual(6, userPosts.Count);
        }

        [TestMethod]
        public void TestAddPostForUser4()
        {
            //ARRANGE:
            const string user = "user5";
            ApplicationUser u5 = new ApplicationUser
            {
                Id = "user5",
            };
            UserPost post = new UserPost
            {
                Body = "Bæta við póst",
                Id = 7,
                User = u5,
                UserId = "user5",
            };

            //ADD:
            _service.AddUserPost(post, user);
            var userPosts = _service.GetPosts();


            //ASSERT:
            Assert.AreEqual(7, userPosts.Count);
        }
    }
}
