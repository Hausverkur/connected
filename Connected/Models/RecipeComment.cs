using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Connected.Models
{
    public class RecipeComment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateTimePosted { get; set; }
        public string AuthorId { get; set; }
        public int RecipeId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }
        [ForeignKey("RecipeId")]
        public virtual Recipe RecipeReference { get; set; }
    }
}