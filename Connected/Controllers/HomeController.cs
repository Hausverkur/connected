using System;
using System.Collections.Generic;
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
            UserPostService postService = new UserPostService();
            CommentService commentService = new CommentService();

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

           return View(frontPage);
        }

        public ActionResult AddPost()
        {
            return View();
        }
        public ActionResult MyWall()
        {
            UserPostService postService = new UserPostService();
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
            UserPostService postService = new UserPostService();
            UserService userService = new UserService();
            UserPost post = new UserPost();
            UpdateModel(post);
            postService.AddUserPost(post, this.User.Identity.GetUserId());
            return RedirectToAction("MyWall");
        }
    }
}