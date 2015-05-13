using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.ViewModels
{
    public class FrontPageViewModel
    {
        public List<UserPostViewModel> Posts { get; set; }
        public UserPost Post { get; set; }
        public List<RequestViewModel> Requests { get; set; } 
    }
}