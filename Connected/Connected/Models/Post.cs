using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Body { get; set; }
        public System.DateTime DateInserted { get; set; }
    }
}