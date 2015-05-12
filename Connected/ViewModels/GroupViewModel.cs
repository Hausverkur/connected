using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int NumberOfUsers { get; set; }
        public bool UserInGroup { get; set; }

        public List<ApplicationUser> Users { get; set; }
        public List<UserPostViewModel> Posts { get; set; } 
    }
}