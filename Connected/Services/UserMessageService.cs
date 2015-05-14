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
        //Hér er skilgreint ef null er parameter í Serviceföllum þá er kallað í hinn raunverulega database
        //Þetta er gert til þess að geta notað unit test á föll í mock-Database
        private readonly IAppDataContext _db;

        public UserMessageService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }
        
        //Fallið nær í lista af öllum skilaboðum sem notandi hefur fengið send
        public List<UserMessage> GetUserRecievedMessages(string userId)
        {
            
            var messages = (from p in _db.UserMessages
                         where p.Recipient.Id == userId
                         select p).ToList();

            return messages;
        }

        //Fallið nær í lista af öllum skilaboðum sem notandinn hefur sent til annarra notanda
        public List<UserMessage> GetUserSentMessages(string userId)
        {
            var messages = (from p in _db.UserMessages
                            where p.Author.Id == userId
                            select p).ToList();

            return messages;
        }

        //Þetta fall sækir nýasta skilboð milli tveggja notanda
        public UserMessageViewModel GetLatestMessageFromUser(string user1Id, string user2Id)
        {
            var message = (from m in _db.UserMessages
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

        //Þetta falla sækir öll skilaboð sem send hafa verið á milli tveggja notanda
        public List<UserMessageViewModel> GetConversation(string user1Id, string user2Id)
        {
            var messages = (from m in _db.UserMessages
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