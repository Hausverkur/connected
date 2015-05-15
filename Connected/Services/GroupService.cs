using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
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
        private readonly IAppDataContext _db;

        public GroupService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }

        //sækir lista af öllum hópum þeir raðast eftir stærð hópmeðlima - stærstu hóparnir efst
        public List<Group> GetListOfGroups()
        {
            var groups = (from g in _db.Groups
                          orderby g.NumberOfUsers descending
                          select g).ToList();

            return groups;
        }

        //Sækir tiltekinn hóp eftir Id
        public Group GetGroupById(int? id)
        {
            if (id.HasValue)
            {
                int theId = id.Value;
                var group = (from g in _db.Groups
                    where g.Id == theId
                    select g).FirstOrDefault();
                return group;
            }
            return null;
        }

        //Fallið býr til nýjann hóp í gagnagrunni frá notanda
        public void AddGroup(Group group, string userId)
        {

            ApplicationDbContext db = new ApplicationDbContext();

            var user = (from u in db.Users
                        where u.Id == userId
                        select u).FirstOrDefault();

            if (user != null)
            {
                Group g = new Group
                {
                    Description = group.Description,
                    Id = group.Id,
                    Image = group.Image,
                    Name = group.Name,
                    NumberOfUsers = group.NumberOfUsers,
                };
                _db.Groups.Add(g);
                _db.SaveChanges();
           }
            else return;
        }

        //Fallið bætir notanda í hóp að beiðni notandans
        public void AddGroupMember(int groupId, string userId)
        {
            _db.GroupMembers.Add(new GroupMember
            {
                GroupId = groupId,
                UserId = userId,
            });

            _db.SaveChanges();
            UpdateGroup(groupId);
        }

        //Þetta fall athugar hvort notandi er þegar í tilteknum hóp og skilar bool
        public bool IsInGroup(int groupId, string userId)
        {
            var inGroup = (from member in _db.GroupMembers
                where member.GroupId == groupId && member.UserId == userId
                select member).ToList();
            if (inGroup.Count > 0)
            {
                return true;
            }
            return false;
        }

        //Notandi fer úr tilteknum hóp með þessu falli
        public void RemoveGroupMember(int groupId, string userId)
        {
            var member = (from m in _db.GroupMembers
                where m.GroupId == groupId && m.UserId == userId
                select m).First();
            _db.GroupMembers.Remove(member);
            _db.SaveChanges();
            UpdateGroup(groupId);
        }

        //Þetta fall update-ar fjölda notanda í hópum
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

        //Þetta fall sækir lista af öllum þeim póstum sem notendur hafa deilt á vegg tiltekins hóps
        public List<UserPostViewModel> GetGroupPostsById(int groupId)
        {
            var posts = (from p in _db.UserPosts
                where p.GroupReference == groupId && p.GroupPost == true
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
                    GroupPost = post.GroupPost,
                    TheGroup = GetGroupById(post.GroupReference),
                    Comments = new List<CommentViewModel>(),
                });

            }
            return groupPosts;
        }

        //Þetta fall býr til póst tiltekins notanda á hópavegg og setur í gagnagrunninn
        public void CreateGroupPost(string userId, int groupId, UserPost post)
        {
            DateTime now = DateTime.Now;

            var userInGroup = (from u in _db.GroupMembers
                                where u.UserId == userId && u.GroupId == groupId 
                                select u).FirstOrDefault();

            if (userInGroup != null)
            {
                {
                    _db.UserPosts.Add(new UserPost
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
                    _db.SaveChanges();
                }
           }
            else return;

        }

        //Sýnir lista af öllum hópum sem tiltekinn notandi er skráður í
        public List<Group> GetGroupsForUser(string userId)
        {
            var members = (from m in _db.GroupMembers
                              where m.UserId == userId
                              select m.GroupId).ToList();

            List<Group> model = new List<Group>();

            foreach (var member in members)
            {
                var group = (from g in _db.Groups
                    where g.Id == member
                    select g).FirstOrDefault();
                model.Add(group);
            }

            return model;
        }

        //Bætir lista af Group Modelum í GroupViewModel
        public List<GroupViewModel> AddGroupModelToVewModel(List<Group> groups)
        {
            List<GroupViewModel> groupsViewModel = new List<GroupViewModel>();

            foreach (var group in groups)
            {
                groupsViewModel.Add(new GroupViewModel
                {
                    Description = group.Description,
                    Id = group.Id,
                    Image = group.Image,
                    Name = group.Name,
                    NumberOfUsers = group.NumberOfUsers,
                });
            }

            return groupsViewModel;
        }

    }
}