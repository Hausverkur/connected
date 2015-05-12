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
    public class UserMessageController : Controller
    {
        // GET: UserMessage
        public ActionResult Index()
        {

            UserMessageService messageService = new UserMessageService();

            UserMessagesViewModel userMessages = new UserMessagesViewModel();

            var userId = this.User.Identity.GetUserId();

            var recieved = messageService.GetUserRecievedMessages(userId);
            
            userMessages.RecievedMessages = new List<UserMessageViewModel>();

            foreach (var message in recieved)
            {
                userMessages.RecievedMessages.Add(new UserMessageViewModel
                {
                    Id = message.Id,
                    Title = message.Title,
                    Body = message.Body,
                    Author = message.Author,
                    DateTimePosted = message.DateTimePosted,
                    Recipient = message.Recipient,
                });
            }

            var sent = messageService.GetUserSentMessages(userId);

            userMessages.SentMessages = new List<UserMessageViewModel>();

            foreach (var message in sent)
            {
                userMessages.SentMessages.Add(new UserMessageViewModel
                {
                    Id = message.Id,
                    Title = message.Title,
                    Body = message.Body,
                    Author = message.Author,
                    DateTimePosted = message.DateTimePosted,
                    Recipient = message.Recipient,
                });
            }
            
            return View(userMessages);
        }
    }
}