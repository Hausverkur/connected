using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Method { get; set; }
        public string Image { get; set; }
        public DateTime DateTimePosted { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        //public virtual ApplicationUser Author { get; set; }
    }
}