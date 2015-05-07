using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Connected.Models;
using Microsoft.Ajax.Utilities;

namespace Connected.Services
{
    public class UserPostService
    {
        public List<UserStatus> GetUserStatuses()
        {
            var db = new ApplicationDbContext();

            var userPosts = (from p in db.UserStatuses
                            // where p.Id == userId
                            select p).ToList();
            return userPosts;
        }
        
    }
}