using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;
using Connected.Services;

namespace Connected.ViewModels
{
    public class UserPostViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public List<CommentViewModel> Comments { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}