using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Connected.Services;
using Connected.ViewModels;

namespace Connected.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            /*SearchService searchService = new SearchService();

            SearchViewModel searchList = new SearchViewModel();

            var users = searchService.GetAllUsers();

            searchList.Users = new List<UserViewModel>();

            foreach (var user in users)
            {
                searchList.Users.Add( new UserViewModel
                {
                    Id = user.Id,
                    Age = user.Age,
                    Description = user.Description,
                    Email = user.Email,
                    Gender = user.Gender,
                    ProfilePicture = user.ProfilePicture,
                    UserName = user.UserName,
                    FullName = user.FullName,                   
                });
            }


            return View(searchList);
        }*/
            return View();
        }

        public ActionResult Search(string search)
        {
            SearchService service = new SearchService();
            SearchViewModel model = new SearchViewModel();

            model.Users = service.GetUsers(search);
            model.Groups = service.GetGroups(search);
            model.Recipes = service.GetRecipes(search);

            return View(model);
        }

        [HttpGet]
        public ActionResult SearchForm()
        {
            return View(new SearchViewModel());
        }

        /*[HttpPost]
        public ActionResult SearchForm(FormCollection formData)
        {
            
        }*/
    }
}
