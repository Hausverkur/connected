﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.ViewModels
{
    public class RecipeCommentViewModel
    {

        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateTimePosted { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}