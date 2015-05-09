using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class GroupPost
    {
        public int Id { get; set; }
        public virtual Group GroupReference { get; set; }
        public virtual UserPost Post { get; set; }
    }
}