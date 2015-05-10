using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;
using Connected.ViewModels;

namespace Connected.Services
{
    
    public class GroupService
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<Group> GetListOfGroups()
        {
            var groups = (from g in db.Groups
                          orderby g.NumberOfUsers descending
                          select g).ToList();

            return groups;
        }

        public Group GetGroupById(int? id)
        {
            if (id.HasValue)
            {
                int theId = id.Value;
                var group = (from g in db.Groups
                    where g.Id == theId
                    select g).First();
                return group;
            }
            return null;
        }
    }
}