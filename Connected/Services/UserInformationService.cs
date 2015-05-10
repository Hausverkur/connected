using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connected.Models;

namespace Connected.Services
{
    public class UserInformationService
    {
        public ApplicationUser GetUserInfo(string userId)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var information = (from i in db.Users
                               where i.Id == userId
                               select i).First();


            return information;
        }
    }
}