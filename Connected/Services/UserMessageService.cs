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
        public List<UserMessage> GetUserRecievedMessages(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            var messages = (from p in db.UserMessages
                         where p.Recipient.Id == userId
                         select p).ToList();

            return messages;
        }
        public List<UserMessage> GetUserSentMessages(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var messages = (from p in db.UserMessages
                            where p.Author.Id == userId
                            select p).ToList();

            return messages;
        }
    }
}