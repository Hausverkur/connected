using System;
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
                Dislikes = recipe.Dislikes,
                Id = recipe.Id,
                Image = recipe.Image,
                Ingredients = recipe.Ingredients,
                Likes = recipe.Likes,
                Method = recipe.Method,
                Name = recipe.Name,
                //Author = recipe.Author
            });

            db.SaveChanges();
        }

        public void CreateComment(string userId, int groupId, UserPost post)
        {
            DateTime now = DateTime.Now;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.UserPosts.Add(new UserPost
                {
                    UserId = userId,
                    Body = post.Body,
                    DateTimePosted = now,
                    Likes = 0,
                    Dislikes = 0,
                    Shares = 0,
                    GroupPost = true,
                    GroupReference = groupId,
                    ImageUrl = post.ImageUrl,
                });
                db.SaveChanges();
            }
        }
    }
}