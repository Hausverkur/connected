using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;
using Connected.ViewModels;

namespace Connected.Services
{
    public class UserMessageService
    {
        public List<UserMessageViewModel> GetUserMessages(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();


            var messages = (from p in db.UserMessages
                         where p.Recipient.Id == userId
                         select p).ToList();

            List<UserMessageViewModel> userMessages = new List<UserMessageViewModel>();
            
            foreach (var message in messages)
            {
                userMessages.Add(new UserMessageViewModel
                {
                    Id = message.Id,
                    Title = message.Title,
                    Body = message.Body,
                    Author = message.Author,
                    DateTimePosted = message.DateTimePosted,

                });
            }
         
            return userMessages;
        }
    }
}