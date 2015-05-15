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
                        //Author = recipe.Author,
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
            service.AddRecipe(recipe);
            return RedirectToAction("ListOfRecipes");
        }

        //comments below
        /*
        [HttpGet]
        public ActionResult DisplayComment(int? id)
        {
            RecipeService service = new RecipeService();
            if (id.HasValue)
            {
                int theId = id.Value;
                var comment = service.GetCommentById(theId);
                var theComment = new RecipeCommentViewModel
                {
                    Body = comment.Body,
                    DateTimePosted = comment.DateTimePosted,
                    Id = comment.Id,
                    //Author = recipe.Author,
                };

                return View(theComment);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(FormCollection formData)
        {
            RecipeService service = new RecipeService();
            Comment comment = new Comment();
            UpdateModel(comment);
            service.AddComment(comment);
            return RedirectToAction("DisplayRecipe");
        }*/

        [HttpGet]
        public ActionResult CreateRecipeComment()
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(new RecipeComment());
            }
        }

        [HttpPost]
        public ActionResult CreateRecipeComment(FormCollection formData, int? id)
        {
            if (id.HasValue)
            {
                RecipeService recipeService = new RecipeService(null);
                RecipeComment comment = new RecipeComment();
                UpdateModel(comment);
                recipeService.CreateRecipeComment(this.User.Identity.GetUserId(), id.Value, comment);
                return RedirectToAction("DisplayRecipe", id.Value);
            }
            return RedirectToAction("ListOfRecipes");
        }

        
    }
}