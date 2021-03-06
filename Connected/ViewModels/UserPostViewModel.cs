﻿using System;
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
        public DateTime DateTimePosted { get; set; }
        public string ImageUrl { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Shares { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public bool GroupPost { get; set; }
        public virtual Group TheGroup { get; set; }
    }
}