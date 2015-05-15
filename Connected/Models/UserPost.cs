using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string ImageUrl { get; set; }
        public int RecipeId { get; set; }

        public bool GroupPost { get; set; }
        public int GroupReference { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}