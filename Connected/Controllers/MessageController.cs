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
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Messages()
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                UserMessageService messageService = new UserMessageService();
                UserService userService = new UserService(null);

                UserMessagesViewModel model = new UserMessagesViewModel();

                model.RecievedMessages = messageService.GetRecievedMessages(this.User.Identity.GetUserId());
                model.SentMessages = messageService.GetSentMessages(this.User.Identity.GetUserId());

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult CreateMessage()
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(new UserMessage());
            }
        }

        [HttpPost]
        public ActionResult CreateMessage(FormCollection formData, string id)
        {
            UserMessageService service = new UserMessageService();
            UserMessage message = new UserMessage
            {
                Title = formData["Title"],
                Body = formData["Body"]
            };
            service.AddMessage(this.User.Identity.GetUserId(), id, message);
            return RedirectToAction("Messages");
        }
    }
}