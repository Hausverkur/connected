using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Connected.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Groups()
        {
            ViewBag.Message = "My groups.";

            return View();
        }

        public ActionResult MyWall()
        {
            ViewBag.Message = "My Wall.";

            return View();
        }

        public ActionResult Messages()
        {
            ViewBag.Message = "My Messages.";

            return View();
        }

        public ActionResult Recipes()
        {
            ViewBag.Message = "My Recipes.";

            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Message = "My what?";

            return View();
        }
    }
}