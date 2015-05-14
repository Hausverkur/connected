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
        private readonly IAppDataContext _db;

        public UserPostService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }

        public List<UserPostViewModel> GetPosts()
        {
            var posts = (from p in _db.UserPosts.OfType<UserPost>()
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
            var posts = (from p in _db.UserPosts
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
            DateTime now = DateTime.Now;
            
                _db.UserPosts.Add(new UserPost
            {
                UserId = userId,
                Body = post.Body,
                DateTimePosted = now,
                Likes = 0,
                Dislikes = 0,
                Shares = 0,
                GroupPost = false,
                GroupReference = 0,
                ImageUrl = post.ImageUrl,
            });
                _db.SaveChanges();
            }
        }

        /*public List<UserPostViewModel> GetFriendsPosts(List<ApplicationUser> friends)
        {
            
        }*/
    }

