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
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Messages()
        {
            UserMessageService messageService = new UserMessageService(null);
            UserService userService = new UserService(null);

            List<UserMessageViewModel> model = new List<UserMessageViewModel>();
            var friends = userService.GetFriends(this.User.Identity.GetUserId());

            foreach (var user in friends)
            {
                model.Add(messageService.GetLatestMessageFromUser(user.Id, this.User.Identity.GetUserId()));
            }


            return View(model);
        }
    }
}