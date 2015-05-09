using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Connected.Models
{
    public class UserPost
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateTimePosted { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Shares { get; set; }
        public bool IsImage { get; set; }
        public bool IsRecipe { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}