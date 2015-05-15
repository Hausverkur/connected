using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Connected.Models;
using Connected.Services;
using Connected.ViewModels;
using Microsoft.AspNet.Identity;

namespace Connected.Controllers
{
    public class GroupController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ListOfGroups()
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<GroupViewModel> groupList = new List<GroupViewModel>();
                GroupService service = new GroupService(null);
    
                var groupModels = service.GetListOfGroups();

                foreach (var group in groupModels)
                {
                    groupList.Add(new GroupViewModel
                    {
                        Description = group.Description,
                        Id = group.Id,
                        Image = group.Image,
                        Name = group.Name,
                        NumberOfUsers = group.NumberOfUsers,
                    });
                }

                return View(groupList);
            }
        }

        [HttpGet]
        public ActionResult DisplayGroup(int? id)
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (id.HasValue)
                {
                    GroupService service = new GroupService(null);
                    int theId = id.Value;
                    var groupModel = service.GetGroupById(theId);
                    GroupViewModel group = new GroupViewModel
                    {
                        Description = groupModel.Description,
                        Id = groupModel.Id,
                        Image = groupModel.Image,
                        Name = groupModel.Name,
                        NumberOfUsers = groupModel.NumberOfUsers,
                        UserInGroup = service.IsInGroup(theId, this.User.Identity.GetUserId()),
                    };

                    group.Posts = service.GetGroupPostsById(theId);

                    group.Posts = group.Posts.OrderByDescending(p => p.Id).ToList();

                    CommentService commentService = new CommentService(null);

                    foreach (var post in group.Posts)
                    {
                        var comments = commentService.GetCommentsByPostId(post.Id);
                        post.Comments = commentService.AddCommentsToViewModel(comments);
                    }

                    return View(group);
                }
                return RedirectToAction("ListOfGroups");
            }
        }

        [HttpGet]
        public ActionResult CreateGroup()
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

        [HttpPost]
        public ActionResult CreateGroup(FormCollection formData)
        {
            GroupService service = new GroupService(null);
            Group group = new Group();
            UpdateModel(group);
            service.AddGroup(group, this.User.Identity.GetUserId());
            return RedirectToAction("ListOfGroups");
        }

        public ActionResult AddGroupMember(int? id)
        {
            if (id.HasValue)
            {
                GroupService service = new GroupService(null);
                service.AddGroupMember(id.Value, this.User.Identity.GetUserId());

                return RedirectToAction("DisplayGroup", new { id = id.Value });
            }
            return RedirectToAction("ListOfGroups");
        }

        public ActionResult RemoveGroupMember(int? id)
        {
            if (id.HasValue)
            {
                GroupService service = new GroupService(null);
                service.RemoveGroupMember(id.Value, this.User.Identity.GetUserId());

                return RedirectToAction("DisplayGroup", new { id = id.Value });
            }
            return RedirectToAction("ListOfGroups");
        }

        [HttpGet]
        public ActionResult CreateGroupPost()
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
        public ActionResult CreateGroupPost(FormCollection formData, int? id)
        {
            if (id.HasValue)
            {
                GroupService groupService = new GroupService(null);
                UserPost post = new UserPost();
                UpdateModel(post);
                groupService.CreateGroupPost(this.User.Identity.GetUserId(), id.Value, post);
                return RedirectToAction("DisplayGroup", id);
            }
            return RedirectToAction("ListOfGroups");
        }

        [HttpGet]
        public ActionResult UserWall(int? id)
        {
            return RedirectToAction("UserWall", new { id = this.User.Identity.GetUserId() });
        }
    }
}