using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connected.Services;
using Connected.ViewModels;

namespace Connected.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe
        public ActionResult ListOfResipes()
        {
            RecipeService service = new RecipeService();

            var recipes = service.GetRecipes();

            List<RecipeViewModel> recipeList = new List<RecipeViewModel>();

            foreach (var recipe in recipes)
            {
                recipeList.Add(new RecipeViewModel
                {
                    DateTimePosted = recipe.DateTimePosted,
                    Description = recipe.Description,
                    Dislikes = recipe.Dislikes,
                    Id = recipe.Id,
                    Image = recipe.Image,
                    Ingredients = recipe.Ingredients,
                    Likes = recipe.Likes,
                    Method = recipe.Method,
                    Name = recipe.Name,
                });
            }

            return View(recipeList);
        }

        [HttpGet]
        public ActionResult DisplayRecipe(int? id)
        {
            RecipeService service = new RecipeService();
            if (id.HasValue)
            {
                int theId = id.Value;
                var recipe = service.GetRecipeById(theId);
                var theRecipe = new RecipeViewModel
                {
                    DateTimePosted = recipe.DateTimePosted,
                    Description = recipe.Description,
                    Dislikes = recipe.Dislikes,
                    Id = recipe.Id,
                    Image = recipe.Image,
                    Ingredients = recipe.Ingredients,
                    Likes = recipe.Likes,
                    Method = recipe.Method,
                    Name = recipe.Name,
                };
                return View(theRecipe);
            }
            return View();
        }
    }
}