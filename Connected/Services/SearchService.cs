using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;
using Connected.ViewModels;

namespace Connected.Services
{
    public class SearchService
    {
        public List<UserViewModel> GetUsers(string searchString)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var users = (from u in db.Users
                select u)
                .Where(u => u.UserName.Contains(searchString))
                .ToList();

            List<UserViewModel> model = new List<UserViewModel>();

            foreach (var user in users)
            {
                model.Add(new UserViewModel
                {
                    FullName = user.FullName,
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfilePicture = user.ProfilePicture,
                });
            }

            return model;
        }

        public List<GroupViewModel> GetGroups(string searchString)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var groups = (from u in db.Groups
                        select u)
                .Where(u => u.Name.Contains(searchString))
                .ToList();

            List<GroupViewModel> model = new List<GroupViewModel>();

            foreach (var group in groups)
            {
                model.Add(new GroupViewModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    Image = group.Image,
                    NumberOfUsers = group.NumberOfUsers,
                });
            }

            return model;
        }

        public List<RecipeViewModel> GetRecipes(string searchString)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var recipes = (from u in db.Recipes
                        select u)
                .Where(u => u.Name.Contains(searchString))
                .ToList();

            List<RecipeViewModel> model = new List<RecipeViewModel>();

            foreach (var recipe in recipes)
            {
                model.Add(new RecipeViewModel
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Description = recipe.Description,
                    Image = recipe.Image,
                });
            }

            return model;
        }
    }
}