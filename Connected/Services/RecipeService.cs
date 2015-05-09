﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.Services
{
    public class RecipeService
    {
        public List<Recipe> GetRecipes()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var recipes = (from recipe in db.Recipes
                           select recipe).ToList();

            return recipes;
        }

        public Recipe GetRecipeById(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var recipe = (from r in db.Recipes
                          where r.Id == id
                          select r).First();

            return recipe;
        }
    }
}