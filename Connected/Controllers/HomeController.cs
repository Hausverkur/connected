using System;
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

        public ActionResult FrontPage()
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
                GroupService groupService = new GroupService(null);

                var friends = userService.GetFriends(this.User.Identity.GetUserId());

                FrontPageViewModel frontPage = new FrontPageViewModel();
                frontPage.Posts = new List<UserPostViewModel>();
                foreach (var friend in friends)
                {
                    frontPage.Posts.AddRange(postService.GetPostsByUserId(friend.Id));
                }

                var groups = groupService.GetGroupsForUser(this.User.Identity.GetUserId());

                foreach (var group in groups)
                {
                    frontPage.Posts.AddRange(groupService.GetGroupPostsById(group.Id));
                }

                frontPage.Posts.AddRange(postService.GetPostsByUserId(this.User.Identity.GetUserId()));

                frontPage.Posts = frontPage.Posts.OrderByDescending(p => p.Id).ToList();

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
        }
        public ActionResult AddPost()
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult MyWall()
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("UserWall", new {id = this.User.Identity.GetUserId()});
            }
        }

        [HttpGet]
        public ActionResult CreateUserPost()
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(new UserPost());
            }
        }

        [HttpPost]
        public ActionResult CreateUserPost(FormCollection formData)
        {
            UserPostService postService = new UserPostService(null);
            
            UserPost post = new UserPost();
            
            UpdateModel(post);
            postService.AddUserPost(post, this.User.Identity.GetUserId());
            return RedirectToAction("MyWall");
            
        }

         public ActionResult UserWall(string id)
        {
             if (this.User.Identity.GetUserId() == null)
             {
                 return RedirectToAction("Index", "Home");
             }
             else
             {
                 if (id == null)
                 {
                     id = this.User.Identity.GetUserId();
                 }
                 UserPostService postService = new UserPostService(null);
                 CommentService commentService = new CommentService(null);
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
            return RedirectToAction("FrontPage");
        }

        public ActionResult DenyFriendRequest(int friendshipId)
        {
            UserService userService = new UserService(null);
            userService.RemoveFriendship(friendshipId);
            return RedirectToAction("FrontPage");
        }

        public ActionResult RemoveFriend(string userId)
        {
            UserService userService = new UserService(null);
            userService.RemoveFriendship(userService.FindFriendship(userId, this.User.Identity.GetUserId()));
            return RedirectToAction("UserWall", new{id = userId});
        }

        [HttpGet]
        public ActionResult EditUserInfo(string userId)
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult CreateComment(int id)
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(new Comment{Id = id});
            }
        }

        [HttpPost]
        public ActionResult CreateComment(FormCollection formData, string returnPath, string returnController)
        {
            UserPostService postService = new UserPostService(null);
            Comment comment = new Comment();
            UpdateModel(comment);
            postService.AddComment(this.User.Identity.GetUserId(), comment);
            return RedirectToAction(returnPath, returnController);
        }

        public ActionResult Information()
        {
            UserService userService = new UserService(null);
            ApplicationUser user = userService.GetUserInfo(this.User.Identity.GetUserId());
            InfoViewModel model = new InfoViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Age = user.Age,
                Description = user.Description,
                Gender = user.Gender,
                Image = user.ProfilePicture
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Information(FormCollection formdata)
        {
            UserService userService = new UserService(null);
            InfoViewModel model = new InfoViewModel();
            UpdateModel(model);
            model.Id = this.User.Identity.GetUserId();
            userService.UpdateUserInfo(model);
            return RedirectToAction("UserWall", new{id = this.User.Identity.GetUserId()});
        }

    }
}
