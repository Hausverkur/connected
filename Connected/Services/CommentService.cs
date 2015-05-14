using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.Services
{
    public class CommentService
    {
        //Hér er skilgreint ef null er parameter í Serviceföllum þá er kallað í hinn raunverulega database
        //Þetta er gert til þess að geta notað unit test á föll í mock-Database
        private readonly IAppDataContext _db;

        public CommentService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }

        //Þetta fall sækir lista af öllum kommentum við tiltekinn póst
        public List<Comment> GetCommentsByPostId(int postId)
        {
            var comments = (from comment in _db.Comments
                where comment.Post.Id == postId
                select comment).ToList();
        
            return comments;
        }
    }
}