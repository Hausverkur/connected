using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.ViewModels
{
    public class GroupListViewModel
    {
        public List<GroupViewModel> MyGroups { get; set; }
        public List<GroupViewModel> ListOfAllGroups { get; set; } 
    }
}