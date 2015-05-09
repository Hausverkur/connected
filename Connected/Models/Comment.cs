using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateTimePosted { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual UserPost Post { get; set; }
    }
}