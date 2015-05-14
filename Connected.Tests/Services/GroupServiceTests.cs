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
    public class GroupServiceTests
    {
        private GroupService _service;

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
            var u5 = new ApplicationUser
            {
                Id = "user5",
            };
            mockDb.ApplicationUsers.Add(u5);
            var g1 = new Group
            {
                Id = 1,
                Name = "Hópur1",
            };
            mockDb.Groups.Add(g1);
            var g2 = new Group
            {
                Id = 2,
                Name = "Hópur2",
            };
            mockDb.Groups.Add(g2);
            var g3 = new Group
            {
                Id = 3,
                Name = "Hópur3",
            };
            mockDb.Groups.Add(g3);
            
            var p1 = new UserPost
            {
                Id = 1,
                Body = "Halló",
                User = u1,
                UserId = "user1",
                GroupReference = 1,
                GroupPost = true,
                DateTimePosted = DateTime.Now,
            };
            mockDb.UserPosts.Add(p1);
            var p2 = new UserPost
            {
                Id = 2,
                Body = "NeiNei",
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
                Body = "Hæ",
                User = u4,
                UserId = "user4",
                GroupReference = 1,
                GroupPost = true,
                DateTimePosted = DateTime.Now,
            };
            mockDb.UserPosts.Add(p3);
            var p4 = new UserPost
            {
                Id = 4,
                Body = "Hola",
                User = u2,
                UserId = "user2",
                GroupReference = 2,
                GroupPost = false,
                DateTimePosted = DateTime.Now,
            };
            mockDb.UserPosts.Add(p4);

            var m1 = new GroupMember
            {
                Id = 1,
                GroupReference = g1,
                GroupId = 1,
                User = u1,
                UserId = "user1",
            };
            mockDb.GroupMembers.Add(m1);
            var m2 = new GroupMember
            {
                Id = 2,
                GroupReference = g1,
                GroupId = 3,
                User = u4,
                UserId = "user4",
            };
            mockDb.GroupMembers.Add(m2);


            _service = new GroupService(mockDb);

        }

        [TestMethod]
        public void TestGetListOfAllGroups()
        {
            //ARRANGE:
            
            //ACT:
            var groups = _service.GetListOfGroups();

            //ASSERT:
            Assert.AreEqual(3, groups.Count);
            
        }

        [TestMethod]
        public void TestGetGroupById()
        {
            //ARRANGE:
            const int groupId = 3;

            //ACT:
            var group = _service.GetGroupById(groupId);

            //ASSERT:
            Assert.AreEqual(3, group.Id);
        }

        public void TestGetGroupByIdNotAvailble()
        {
            //ARRANGE:
            const int groupId = 4;

            //ACT:
            var group = _service.GetGroupById(groupId);

            //ASSERT:
            Assert.AreEqual(null, group);
        }

        [TestMethod]
        public void TestAddGroupWithUser2()
        {
            //ARRANGE:
            const string user = "user1";

            Group g4 = new Group
            {
                Id = 4,
                Name = "Hópur4"
            };
            
            //ADD:
            _service.AddGroup(g4, user);
            var groups = _service.GetListOfGroups();


            //ASSERT:
            Assert.AreEqual(4, groups.Count);
        }
        
        [TestMethod]
        public void TestAddGroupWithNonExistingUser()
        {
            //ARRANGE:
            const string user = "user7";

            Group g4 = new Group
            {
                Id = 4,
                Name = "Hópur4"
            };

            //ADD:
            _service.AddGroup(g4, user);
            var groups = _service.GetListOfGroups();


            //ASSERT:
            Assert.AreEqual(3, groups.Count);
        }

        [TestMethod]
        public void TestAddUserToGroup()
        {
            //ARRANGE:
            const int groupId = 1;
            const string userId = "user4";

            //ACT:
            _service.AddGroupMember(groupId, userId);
            var userIsInGroup = _service.IsInGroup(groupId, userId);

            //ASSERT:
            Assert.AreEqual(true, userIsInGroup);
        }

        [TestMethod]
        public void TestRemoveUserFromGroup()
        {
            //ARRANGE:
            const int groupId = 1;
            const string userId = "user1";

            //ACT:
            _service.RemoveGroupMember(groupId, userId);
            var userIsInGroup = _service.IsInGroup(groupId, userId);

            //ASSERT:
            Assert.AreEqual(false, userIsInGroup);
        }

        [TestMethod]
        public void TestGetGroupPostsById1()
        {
            //ARRANGE:
            const int groupId = 1;
           
            //ACT:
           var postsInGroup = _service.GetGroupPostsById(groupId);

            //ASSERT:
            Assert.AreEqual(2, postsInGroup.Count);
        }

        [TestMethod]
        public void TestGetGroupPostsById2()
        {
            //ARRANGE:
            const int groupId = 2;

            //ACT:
            var postsInGroup = _service.GetGroupPostsById(groupId);

            //ASSERT:
            Assert.AreEqual(0, postsInGroup.Count);
        }

        [TestMethod]
        public void TestCreateGroupPostWithUserInGroup()
        {
            //ARRANGE:
            const string userId = "user4";
            const int groupId = 3;
            UserPost post = new UserPost
            {
                Body = "Hæ Hópur3",
                GroupPost = true,
            };

            //ACT:
            _service.CreateGroupPost(userId, groupId, post);
            var postsInGroup = _service.GetGroupPostsById(groupId);

            //ASSERT:
            Assert.AreEqual(1, postsInGroup.Count);
            foreach (var p in postsInGroup)
            {
                Assert.AreEqual(p.Body, "Hæ Hópur3");
            }
        }

        [TestMethod]
        public void TestCreateGroupPostWithUserNotInGroup()
        {
            //ARRANGE:
            const string userId = "user3";
            const int groupId = 3;
            UserPost post = new UserPost
            {
                Body = "Hæ Hópur3",
                GroupPost = true,
            };

            //ACT:
            _service.CreateGroupPost(userId, groupId, post);
            var postsInGroup = _service.GetGroupPostsById(groupId);

            //ASSERT:
            Assert.AreEqual(0, postsInGroup.Count);
        }
    }
}
