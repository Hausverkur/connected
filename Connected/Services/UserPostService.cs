using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public bool UrlExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TURE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }
        

        public void AddUserPost(UserPost post, string userId)
        {
            DateTime now = DateTime.Now;

            if (UrlExists(post.ImageUrl) == false)
            {
                post.ImageUrl = ".../Connected/Images/Profile.png";
            }
           
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

        public void AddComment(string userId, int postId, Comment comment)
        {
            _db.Comments.Add(new Comment
            {
                AuthorId = userId,
                Body = comment.Body,
                DateTimePosted = DateTime.Now,
                PostId = postId,
            });
            _db.SaveChanges();
        }
    }
}

