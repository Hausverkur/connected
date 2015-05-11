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

        public void AddRecipe(Recipe recipe)
        {
            ApplicationDbContext db = new ApplicationDbContext();


            db.Recipes.Add(new Recipe
            {
                DateTimePosted = DateTime.Now,
                Description = recipe.Description,
                Dislikes = 0,
                Id = recipe.Id,
                Image = recipe.Image,
                Ingredients = recipe.Ingredients,
                Likes = 0,
                Method = recipe.Method,
                Name = recipe.Name,
                //Author = recipe.Author
            });

            db.SaveChanges();
        }

        public void AddComment(Comment comment) //vantar meira?
        {
            ApplicationDbContext db = new ApplicationDbContext();

            db.Comments.Add(new Comment
            {
                Body = comment.Body,
                Id = comment.Id,
                DateTimePosted = DateTime.Now,
               // Author = comment.Author,
            });
            db.SaveChanges();
            
        }
    }
}