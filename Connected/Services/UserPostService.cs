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
            var posts = (from p in db.UserPosts.OfType<UserPost>()
                         select p).ToList();
            List<UserPostViewModel> userPosts = new List<UserPostViewModel>();
            
            foreach (var post in posts)
            {
                userPosts.Add(new UserPostViewModel
                {
                    Id = post.Id,
                    Body = post.Body,
                    Author = post.User,
                });
            }

            CommentService commentService = new CommentService();

            return userPosts;
        }
        public List<UserPostViewModel> GetPostsByUserId(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var posts = (from p in db.UserPosts
                         where p.User.Id == userId
                         select p).ToList();
            List<UserPostViewModel> userPosts = new List<UserPostViewModel>();

            foreach (var post in posts)
            {
                userPosts.Add(new UserPostViewModel
                {
                    Id = post.Id,
                    Body = post.Body,
                    Author = post.User,
                });
            }

            CommentService commentService = new CommentService();

            return userPosts;
        }

        public void AddUserPost(UserPost post, string userId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.UserPosts.Add(new UserPost
            {
                UserId = userId,
                Body = "blablablabla",
                DateTimePosted = DateTime.Now,
                Likes = 0,
                Dislikes = 0,
                Shares = 0,
                GroupPost = false,
                GroupReference = 0,
                ImageUrl = "bla",
            });
                db.SaveChanges();
            }
        }

        /*public List<UserPostViewModel> GetFriendsPosts(List<ApplicationUser> friends)
        {
            
        }*/
    }
}