using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.Services
{
    public class RecipeService
    {
        //Hér er skilgreint ef null er parameter í Serviceföllum þá er kallað í hinn raunverulega database
        //Þetta er gert til þess að geta notað unit test á föll í mock-Database
        private readonly IAppDataContext _db;

        public RecipeService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }

        //Þetta fall sækir lista af öllum uppskriftum í gagnagrunninum
        public List<Recipe> GetRecipes()
        {
            var recipes = (from recipe in _db.Recipes
                           select recipe).ToList();

            return recipes;
        }

        //Þetta fall sækir tiltekna uppskrift eftir Id
        public Recipe GetRecipeById(int id)
        {
            var recipe = (from r in _db.Recipes
                          where r.Id == id
                          select r).First();

            return recipe;
        }

        //Þetta fall bætir við uppskrift sem notandi býr til og setur í gagnagrunninn 
        public void AddRecipe(Recipe recipe)
        {
            _db.Recipes.Add(new Recipe
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

            _db.SaveChanges();
        }

        //Þetta fall setur inn athugasemd/comment við uppskrift sem notandi hefur sett inn
        public void CreateRecipeComment(string userId, RecipeComment comment)
        {
            DateTime now = DateTime.Now;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.RecipeComments.Add(new RecipeComment
                {
                    AuthorId = userId,
                    Body = comment.Body,
                    DateTimePosted = now,
                    RecipeId = comment.RecipeId,
                });
                db.SaveChanges();
            }
        }
    }
}