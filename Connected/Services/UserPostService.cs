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
        //Hér er skilgreint ef null er parameter í Serviceföllum þá er kallað í hinn raunverulega database
        //Þetta er gert til þess að geta notað unit test á föll í mock-Database
        private readonly IAppDataContext _db;

        public UserPostService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }

        //Þetta fall nær í alla pósta úr gagnagrunni sem allir notendur hafa sett inn og skilar lista af þeim í UserPostViewModel.
        //Þetta fall var notast við þegar unnið var að forritinu
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
            
            return userPosts;
        }

        //Þetta fall nær í alla pósta sem tiltekinn notandi hefur sett inn og skilar þeim í lista af UserPostViewModelum
        //ViewModelið er svo nýtt til þess að birta gögnin í gegnum FrontPageViewModel
        public List<UserPostViewModel> GetPostsByUserId(string userId)
        {
            GroupService groupService = new GroupService(null);

            var posts = (from p in _db.UserPosts
                         where p.User.Id == userId
                         && p.GroupPost == false
                         select p).ToList().OrderByDescending(p=>p.Id);
            List<UserPostViewModel> userPosts = new List<UserPostViewModel>();

            foreach (var post in posts)
            {
                userPosts.Add(new UserPostViewModel
                {
                    Id = post.Id,
                    Body = post.Body,
                    Author = post.User,
                    DateTimePosted = post.DateTimePosted,
                    ImageUrl = post.ImageUrl,
                    GroupPost = post.GroupPost,
                    TheGroup = groupService.GetGroupById(post.GroupReference),
                });
            }

            CommentService commentService = new CommentService(null);

            return userPosts;
        }

        //Þetta fall athugar hvort að urlið sem gefið er í Posts, Recipe og ProfilePicture sé ekki örugglega url á valid mynd -  skilar bool
        public bool UrlExists(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                
                request.Method = "HEAD";
                
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
               
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                
                return false;
            }
        }
        
        //Þetta fall bætir inn UserPost sem berast frá notenduum í gagnagrunninn
        public void AddUserPost(UserPost post, string userId)
        {
            DateTime now = DateTime.Now;
            if (post.ImageUrl != null)
            {
                if (UrlExists(post.ImageUrl) == false)
                {
                    post.ImageUrl = "../../Images/NoImage.png";
                }
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

        //Þetta fall bætir athugasemdum/comment-um við pósta í gagnagrunnin sem berast frá notendum.
        public void AddComment(string userId, Comment comment)
        {
            _db.Comments.Add(new Comment
            {
                AuthorId = userId,
                Body = comment.Body,
                DateTimePosted = DateTime.Now,
                PostId = comment.Id,
            });
            _db.SaveChanges();
        }
    }
}

