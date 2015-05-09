using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class RecipePost : UserPost
    {
        public virtual Recipe RecipeReference { get; set; }
    }
}