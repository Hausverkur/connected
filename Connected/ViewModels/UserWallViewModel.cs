using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.ViewModels
{
    public class UserWallViewModel
    {
        public int Id { get; set; }
        public List<UserPostViewModel> Posts { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}