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
        ApplicationDbContext db = new ApplicationDbContext();
        public List<UserPostViewModel> GetPosts()
        {
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
            var posts = (from p in db.UserPosts
                         where p.User.Id == userId
                         && p.GroupPost == false
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
                GroupPost = false,
                GroupReference = 0,
                ImageUrl = post.ImageUrl,
            });
                db.SaveChanges();
            }
        }

        public void AddComment(string userId, int postId, Comment comment)
        {
            db.Comments.Add(new Comment
            {
                AuthorId = userId,
                Body = comment.Body,
                DateTimePosted = DateTime.Now,
                PostId = postId,
            });
            db.SaveChanges();
        }

        /*public List<UserPostViewModel> GetFriendsPosts(List<ApplicationUser> friends)
        {
            
        }*/
    }
}