using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Connected.Services;
using Connected.ViewModels;
using Microsoft.AspNet.Identity;

namespace Connected.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Search(string search)
        {
            if (this.User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                SearchService service = new SearchService();
                SearchViewModel model = new SearchViewModel();

                model.Users = service.GetUsers(search);
                model.Groups = service.GetGroups(search);
                model.Recipes = service.GetRecipes(search);

                return View(model);
            }
        }
        /*[HttpPost]
        public ActionResult SearchForm(FormCollection formData)
        {
            
        }*/
    }
}
