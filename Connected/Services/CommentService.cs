using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.Services
{
    public class CommentService
    {
        public List<Comment> GetCommentsByPostId(UserPost post)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var comments = (from comment in db.Comments
                where comment.Post == post
                select comment).ToList();
        
            return comments;
        }
    }
}