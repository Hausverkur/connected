using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;
using Connected.ViewModels;

namespace Connected.Services
{
    public class UserPostService
    {
        public List<UserPostViewModel> GetPosts()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var posts = (from p in db.Userposts
                         select p).ToList();
            List<UserPostViewModel> userPosts = new List<UserPostViewModel>();

            foreach (var post in posts)
            {
                userPosts.Add(new UserPostViewModel
                {
                    Id = post.Id,
                    Body = post.Body,
                });
            }

            CommentService commentService = new CommentService();

            return userPosts;
        }
    }
}