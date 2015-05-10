using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.ViewModels
{
    public class UserInformationViewModel
    {
        public virtual ApplicationUser User { get; set; }
    }
}