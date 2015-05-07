using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class UserPost
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        public System.DateTime DateInserted { get; set; }
    }
}