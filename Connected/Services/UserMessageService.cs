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
        ApplicationDbContext db = new ApplicationDbContext();
        public List<UserMessage> GetUserRecievedMessages(string userId)
        {
            
            var messages = (from p in db.UserMessages
                         where p.Recipient.Id == userId
                         select p).ToList();

            return messages;
        }
        public List<UserMessage> GetUserSentMessages(string userId)
        {
            var messages = (from p in db.UserMessages
                            where p.Author.Id == userId
                            select p).ToList();

            return messages;
        }

        public UserMessageViewModel GetLatestMessageFromUser(string user1Id, string user2Id)
        {
            var message = (from m in db.UserMessages
                where (m.AuthorId == user1Id && m.RecipientId == user2Id)
                      || (m.AuthorId == user2Id && m.RecipientId == user2Id)
                select m).OrderByDescending(m => m.DateTimePosted).FirstOrDefault();
            if (message != null)
            {
                return new UserMessageViewModel
                {
                    Author = message.Author,
                    Body = message.Body,
                    DateTimePosted = message.DateTimePosted,
                    Id = message.Id,
                    Recipient = message.Recipient,
                };
            }
            else return null;
        }

        public List<UserMessageViewModel> GEtConversation(string user1Id, string user2Id)
        {
            var messages = (from m in db.UserMessages
                where (m.AuthorId == user1Id && m.RecipientId == user2Id)
                      || (m.AuthorId == user2Id && m.RecipientId == user2Id)
                select m).OrderBy(m => m.DateTimePosted).ToList();

            List<UserMessageViewModel> list = new List<UserMessageViewModel>();

            foreach (var message in messages)
            {
                list.Add(new UserMessageViewModel
                {
                    Author = message.Author,
                    Body = message.Body,
                    DateTimePosted = message.DateTimePosted,
                    Id = message.Id,
                    Recipient = message.Recipient,
                });
            }
            return list;
        }
    }
}