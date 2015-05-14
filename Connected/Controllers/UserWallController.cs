using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connected.Models;
using Connected.Services;
using Connected.ViewModels;
using Microsoft.AspNet.Identity;

namespace Connected.Controllers
{
    public class UserWallController : Controller
    {

        public ActionResult UserWall(string id)
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                UserPostService postService = new UserPostService(null);
                CommentService commentService = new CommentService(null);
                UserService userService = new UserService(null);

                var userId = this.User.Identity.GetUserId();

                var posts = postService.GetPostsByUserId(userId);

                UserWallViewModel model = new UserWallViewModel
                {
                    Posts = posts,
                };

                var user = userService.GetUserInfo(userId);
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

                var friends = userService.GetFriends(userId);
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

                model.AreFriends = userService.AreFriends(this.User.Identity.GetUserId(), model.User.Id);

                return View(model);
            }
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
    }
}