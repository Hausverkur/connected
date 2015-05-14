﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Connected.Models;
using Connected.Services;
using Connected.ViewModels;
using Microsoft.AspNet.Identity;

namespace Connected.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts()
        {
            UserPostService postService = new UserPostService(null);
            CommentService commentService = new CommentService();
            UserService userService = new UserService(null);

            var posts = postService.GetPosts();

            FrontPageViewModel frontPage = new FrontPageViewModel
            {
                Posts = posts,
            };

            foreach (var post in frontPage.Posts)
            {
                var Comments = commentService.GetCommentsByPostId(post.Id);
                List<CommentViewModel> commentViewModels = new List<CommentViewModel>();
                foreach (var comment in Comments)
                {
                    commentViewModels.Add(new CommentViewModel
                    {
                        Body = comment.Body,
                        Id = comment.Id,
                        DateTimePosted = comment.DateTimePosted,
                        Author = comment.Author,
                    });                    
                }
                post.Comments = commentViewModels;
            }
            var friendRequests = userService.GetFriendRequests(this.User.Identity.GetUserId());

            frontPage.Requests = new List<RequestViewModel>();

            foreach (var request in friendRequests)
            {
                frontPage.Requests.Add(new RequestViewModel
                {
                    Friendship = request,
                });
            }

           return View(frontPage);
        }

        public ActionResult AddPost()
        {
            return View();
        }
        public ActionResult MyWall()
        {
            UserPostService postService = new UserPostService(null);
            CommentService commentService = new CommentService();

            var userId = this.User.Identity.GetUserId();

            var posts = postService.GetPostsByUserId(userId);

            FrontPageViewModel frontPage = new FrontPageViewModel
            {
                Posts = posts,
            };

            foreach (var post in frontPage.Posts)
            {
                var Comments = commentService.GetCommentsByPostId(post.Id);
                List<CommentViewModel> commentViewModels = new List<CommentViewModel>();
                foreach (var comment in Comments)
                {
                    commentViewModels.Add(new CommentViewModel
                    {
                        Body = comment.Body,
                        Id = comment.Id,
                        DateTimePosted = comment.DateTimePosted,
                        Author = comment.Author,
                    });
                }
                post.Comments = commentViewModels;
            }
            return View(frontPage);
        }

        public ActionResult Messages()
        {
            ViewBag.Message = "Your page.";

            return View();
        }

        public ActionResult Groups()
        {
            ViewBag.Message = "Your page.";

            return View();
        }

        public ActionResult Recipes()
        {
            ViewBag.Message = "Your page.";

            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Your page.";

            return View();
        }

        [HttpGet]
        public ActionResult CreateUserPost()
        {
            return View(new UserPost());
        }

        [HttpPost]
        public ActionResult CreateUserPost(FormCollection formData)
        {
            UserPostService postService = new UserPostService(null);
            UserService userService = new UserService(null);
            UserPost post = new UserPost();
            UpdateModel(post);
            postService.AddUserPost(post, this.User.Identity.GetUserId());
            return RedirectToAction("MyWall");
        }

        [HttpGet]
        public ActionResult AddUserPost()
        {
            return View(new FrontPageViewModel());
        }

        [HttpPost]
        public ActionResult AddUserPost(FormCollection formData)
        {
            UserPostService postService = new UserPostService(null);
            UserService userService = new UserService(null);
            UserPost post = new UserPost();
            UpdateModel(post);
            postService.AddUserPost(post, this.User.Identity.GetUserId());
            return RedirectToAction("MyWall");
        }

         public ActionResult UserWall(string id)
        {
             if (id == null)
             {
                 id = this.User.Identity.GetUserId();
             }
            UserPostService postService = new UserPostService(null);
            CommentService commentService = new CommentService();
            UserService userService = new UserService(null);

            var posts = postService.GetPostsByUserId(id);



            UserWallViewModel model = new UserWallViewModel
            {
                Posts = posts,
            };

            var user = userService.GetUserInfo(id);
            model.User = user;

            foreach (var post in model.Posts)
            {
                var Comments = commentService.GetCommentsByPostId(post.Id);
                List<CommentViewModel> commentViewModels = new List<CommentViewModel>();
                foreach (var comment in Comments)
                {
                    commentViewModels.Add(new CommentViewModel
                    {
                        Body = comment.Body,
                        Id = comment.Id,
                    });
                }
                post.Comments = commentViewModels;
            }

            var friends = userService.GetFriends(id);
            model.Friends = new List<UserViewModel>();
          
            foreach (var u in friends)
            {

                model.Friends.Add(new UserViewModel
                {
                    Age = u.Age,
                    Description = u.Description,
                    Email = u.Email,
                    Gender = u.Gender,
                    ProfilePicture = u.ProfilePicture,
                    UserName = u.UserName,
                    Id = u.Id

                });
            }
             if (id == this.User.Identity.GetUserId())
             {
                 model.AreFriends = 4;
             }
             else
             {
                 model.AreFriends = userService.AreFriends(this.User.Identity.GetUserId(), id);
             }

            return View(model);
        }

        public ActionResult AddFriend(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            db.Friendships.Add(new Friendship
            {
                Comfirmed = false,
                User1Id = this.User.Identity.GetUserId(),
                User2Id = userId,
            });
            db.SaveChanges();

            return RedirectToAction("UserWall", new{id = userId});
        }

        public ActionResult AcceptFriendRequest(int friendshipId)
        {
            UserService userService = new UserService(null);
            userService.AcceptRequest(friendshipId);
            return RedirectToAction("Posts");
        }

        public ActionResult DenyFriendRequest(int friendshipId)
        {
            UserService userService = new UserService(null);
            userService.RemoveFriendship(friendshipId);
            return RedirectToAction("Posts");
        }

        public ActionResult RemoveFriend(string userId)
        {
            UserService userService = new UserService(null);
            userService.RemoveFriendship(userService.FindFriendship(userId, this.User.Identity.GetUserId()));
            return RedirectToAction("UserWall", new{id = userId});
        }
    }
   }