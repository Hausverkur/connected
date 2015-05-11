using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connected.Models;
using Connected.Services;
using Connected.ViewModels;

namespace Connected.Controllers
{
    public class GroupController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Group
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListOfGroups()
        {
           List<GroupViewModel> groupList = new List<GroupViewModel>();

            GroupService service = new GroupService();

            var groupModels = service.GetListOfGroups();

            foreach (var group in groupModels)
            {
                groupList.Add(new GroupViewModel
                {
                    Description = group.Description,
                    Id = group.Id,
                    Image = group.Image,
                    Name = group.Name,
                    NumberOfUsers = group.NumberOfUsers,
                });
            }

            return View(groupList);
        }

        [HttpGet]
        public ActionResult DisplayGroup(int? id)
        {
            if (id.HasValue)
            {
                GroupService service = new GroupService();
                int theId = id.Value;
                var groupModel = service.GetGroupById(theId);
                GroupViewModel group = new GroupViewModel
                {
                    Description = groupModel.Description,
                    Id = groupModel.Id,
                    Image = groupModel.Image,
                    Name = groupModel.Name,
                    NumberOfUsers = groupModel.NumberOfUsers,
                };

                return View(group);
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGroup(FormCollection formData)
        {
            GroupService service = new GroupService();
            Group group = new Group();
            UpdateModel(group);
            service.AddGroup(group);
            return View();
        }
    }
}