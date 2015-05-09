using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.Services
{
    public class CommentService
    {
        public List<Comment> GetCommentsByPostId(int postId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var comments = (from comment in db.Comments
                where comment.Post.Id == postId
                select comment).ToList();
        
            return comments;
        }
    }
}