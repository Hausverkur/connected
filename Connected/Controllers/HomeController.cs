using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connected.Services;
using Connected.ViewModels;

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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}