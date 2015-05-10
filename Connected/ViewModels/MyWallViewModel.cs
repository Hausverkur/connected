using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.ViewModels
{
    public class MyWallViewModel
    {
        public List<UserPostViewModel> Posts { get; set; }
        public UserInformationViewModel UserInfo { get; set; }
    }
}