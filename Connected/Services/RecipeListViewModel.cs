using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.ViewModels;

namespace Connected.Services
{
    public class RecipeListViewModel
    {
        public List<RecipeViewModel> Recipes { get; set; }
    }
}