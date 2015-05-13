﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connected.Models;
using Connected.Services;
using Connected.ViewModels;

namespace Connected.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe
        public ActionResult ListOfRecipes()
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
                    //Author = recipe.Author,
                };

                return View(theRecipe);
            }

            return View();
        }

        [HttpGet]
        public ActionResult CreateRecipe()
        {
            return View(new Recipe());
        }

        [HttpPost]
        public ActionResult CreateRecipe(FormCollection formData)
        {
            RecipeService service = new RecipeService();
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
    }
}