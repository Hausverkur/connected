using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connected.Models;
using Connected.ViewModels;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Ast.Selectors;

namespace Connected.Services
{
    
    public class GroupService
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<Group> GetListOfGroups()
        {
            var groups = (from g in db.Groups
                          orderby g.NumberOfUsers descending
                          select g).ToList();

            return groups;
        }

        public Group GetGroupById(int? id)
        {
            if (id.HasValue)
            {
                int theId = id.Value;
                var group = (from g in db.Groups
                    where g.Id == theId
                    select g).First();
                return group;
            }
            return null;
        }

        public void AddGroup(Group group, string userId)
        {
            Group g = new Group
            {
                Description = group.Description,
                Id = group.Id,
                Image = group.Image,
                Name = group.Name,
                NumberOfUsers = group.NumberOfUsers,
            };
            db.Groups.Add(g);
            db.SaveChanges();
        }

        public void AddGroupMember(int groupId, string userId)
        {
            db.GroupMembers.Add(new GroupMember
            {
                GroupId = groupId,
                UserId = userId,
            });

            db.SaveChanges();
            UpdateGroup(groupId);
        }

        public bool IsInGroup(int groupId, string userId)
        {
            var inGroup = (from member in db.GroupMembers
                where member.GroupId == groupId && member.UserId == userId
                select member).ToList();
            if (inGroup.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void RemoveGroupMember(int groupId, string userId)
        {
            var member = (from m in db.GroupMembers
                where m.GroupId == groupId && m.UserId == userId
                select m).First();
            db.GroupMembers.Remove(member);
            db.SaveChanges();
            UpdateGroup(groupId);
        }

        public void UpdateGroup(int groupId)
        {
            SqlConnection con = new SqlConnection("Data Source=hrnem.ru.is;Initial Catalog=VERK2015_H45;Persist Security Info=True;User ID=VERK2015_H45_usr;Password=lumpysoup85");
            SqlCommand command = new SqlCommand();
            con.Open();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateGroup";
            command.Connection = con;

            SqlParameter param = new SqlParameter
            {
                ParameterName = "groupId",
                Value = groupId,
            };
            command.Parameters.Add(param);
            command.ExecuteNonQuery();
            con.Close();
        }

        public List<UserPostViewModel> GetGroupPostsById(int groupId)
        {
            var posts = (from p in db.UserPosts
                where p.GroupReference == groupId
                select p).ToList();
            List<UserPostViewModel> groupPosts = new List<UserPostViewModel>();
            foreach (var post in posts)
            {
                groupPosts.Add(new UserPostViewModel
                {
                    Author = post.User,
                    Body = post.Body,
                    DateTimePosted = post.DateTimePosted,
                    Likes = post.Likes,
                    Dislikes = post.Dislikes,
                    Shares = post.Shares,
                    Id = post.Id,
                    ImageUrl = post.ImageUrl,
                    Comments = new List<CommentViewModel>(),
                });

            }
            return groupPosts;
        }

        public void CreateGroupPost(string userId, int groupId, UserPost post)
        {
            DateTime now = DateTime.Now;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.UserPosts.Add(new UserPost
                {
                    UserId = userId,
                    Body = post.Body,
                    DateTimePosted = now,
                    Likes = 0,
                    Dislikes = 0,
                    Shares = 0,
                    GroupPost = true,
                    GroupReference = groupId,
                    ImageUrl = post.ImageUrl,
                });
                db.SaveChanges();
            }
        }

        public List<Group> GetGroupsForUser(string userId)
        {
            var members = (from m in db.GroupMembers
                              where m.UserId == userId
                              select m.GroupId).ToList();

            List<Group> model = new List<Group>();

            foreach (var member in members)
            {
                var group = (from g in db.Groups
                    where g.Id == member
                    select g).FirstOrDefault();
                model.Add(group);
            }

            return model;
        }

    }
}