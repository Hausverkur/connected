using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connected.Services;
using Connected.ViewModels;
using Microsoft.AspNet.Identity;

namespace Connected.Controllers
{
    public class UserWallController : Controller
    {
        // GET: UserWall
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserWall()
        {
            UserPostService postService = new UserPostService();
            CommentService commentService = new CommentService();
            UserInformationService infoService = new UserInformationService();

            var userId = this.User.Identity.GetUserId();

            var posts = postService.GetPostsByUserId(userId);



            UserWallViewModel model = new UserWallViewModel
            {
                Posts = posts,
            };

            var user = infoService.GetUserInfo(userId);
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

            return View(model);
        }
    }
}