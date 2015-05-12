using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateTimePosted { get; set; }

        public string AuthorId { get; set; }
        public int PostId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }
        [ForeignKey("PostId")]
        public virtual UserPost Post { get; set; }
    }
}