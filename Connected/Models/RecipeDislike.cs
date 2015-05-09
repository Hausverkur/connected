using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class RecipeDislike
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Recipe RecipeReference { get; set; }
    }
}