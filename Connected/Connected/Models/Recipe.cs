using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string Method { get; set; }
        public string Image { get; set; }
        public System.DateTime TimePosted { get; set; }
    }
}