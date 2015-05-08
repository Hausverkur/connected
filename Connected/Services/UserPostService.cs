using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.Services
{
    public class UserPostService
    {
        public List<UserPost> GetPosts()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var posts = (from p in db.Userposts
                         select p).ToList();

            return posts;
        }
    }
}