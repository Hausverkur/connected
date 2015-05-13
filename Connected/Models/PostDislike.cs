using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class PostDislike
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("PostId")]
        public virtual UserPost Post { get; set; }
    }
}