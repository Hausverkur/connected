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
                    Author = post.Author,
                });
            }

            CommentService commentService = new CommentService();

            return userPosts;
        }
        public List<UserPostViewModel> GetPostsByUserId(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var posts = (from p in db.UserPosts
                         where p.Author.Id == userId
                         select p).ToList();
            List<UserPostViewModel> userPosts = new List<UserPostViewModel>();

            foreach (var post in posts)
            {
                userPosts.Add(new UserPostViewModel
                {
                    Id = post.Id,
                    Body = post.Body,
                    Author = post.Author,
                });
            }

            CommentService commentService = new CommentService();

            return userPosts;
        }

        public void AddUserPost(UserPost userPost)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            db.UserPosts.Add(new UserPost
            {
                DateTimePosted = DateTime.Now,
                Body = userPost.Body,
                Dislikes = 0,
                Likes = 0,
                Shares = 0,
                ImageUrl = userPost.ImageUrl,
                Author = userPost.Author,
            });
            db.SaveChanges();
        }

        /*public List<UserPostViewModel> GetFriendsPosts(List<ApplicationUser> friends)
        {
            
        }*/
    }
}