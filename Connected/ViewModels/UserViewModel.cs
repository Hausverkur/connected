using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

       
        //public virtual ApplicationUser User { get; set; }
    }
}