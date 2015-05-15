using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Connected.Models;
using Connected.Services;
using Connected.ViewModels;
using Microsoft.AspNet.Identity;

namespace Connected.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe
        public ActionResult ListOfRecipes()
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                RecipeService service = new RecipeService(null);

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
        }


        [HttpGet]
        public ActionResult DisplayRecipe(int? id)
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                RecipeService service = new RecipeService(null);
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
                        Author = recipe.Author,
                    };
                    theRecipe.Comments = new List<RecipeCommentViewModel>();
                    return View(theRecipe);
                }

                return RedirectToAction("ListOfRecipes");
            }
        }

        [HttpGet]
        public ActionResult CreateRecipe()
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(new Recipe());
            }
        }

        [HttpPost]
        public ActionResult CreateRecipe(FormCollection formData)
        {
            RecipeService service = new RecipeService(null);
            Recipe recipe = new Recipe();
            UpdateModel(recipe);
            service.AddRecipe(recipe, this.User.Identity.GetUserId());
            return RedirectToAction("ListOfRecipes");
        }

        [HttpGet]
        public ActionResult CreateRecipeComment(int id)
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(new CommentViewModel{Id = id,});
            }
        }

        [HttpPost]
        public ActionResult CreateRecipeComment(FormCollection formData)
        {
                RecipeService recipeService = new RecipeService(null);
                CommentViewModel comment = new CommentViewModel();
                UpdateModel(comment);
                recipeService.CreateRecipeComment(this.User.Identity.GetUserId(), comment);
                return RedirectToAction("DisplayRecipe", comment.Id);
        }

        [HttpGet]
        public ActionResult PostRecipe(int id)
        {
            RecipeService service = new RecipeService(null);
            return View(new UserPost {Recipe = service.GetRecipeById(id)});
        }

        [HttpPost]
        public ActionResult PostRecipe(FormCollection formData)
        {
            RecipeService recipeService = new RecipeService(null);
            UserPost post = new UserPost();
            UpdateModel(post);
            recipeService.CreateRecipePost(this.User.Identity.GetUserId(), post);
            return RedirectToAction("DisplayRecipe", new{id = post.Recipe.Id});
        }
    }
}