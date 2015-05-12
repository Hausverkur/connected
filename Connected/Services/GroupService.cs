using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connected.Models;
using Connected.ViewModels;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        }
    }
}