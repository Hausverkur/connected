using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.ViewModel
{
    public class UserPostViewModel
    {
        public string Body { get; set; }
        public System.DateTime TimeInserted { get; set; }

		//public List<CommentViewModel> Comments {get; set;}
		// og fleira sem dæmi userinn sem postaði
    }
}