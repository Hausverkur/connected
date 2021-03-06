﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Connected.ViewModels
{
    public class SearchViewModel
    {
        public string Id { get; set; }
        public List<UserViewModel> Users { get; set; }
        public List<GroupViewModel> Groups { get; set; }
        public List<RecipeViewModel> Recipes { get; set; }
    }
}