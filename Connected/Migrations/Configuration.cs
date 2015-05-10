using System.Collections.Generic;
using Connected.Models;

namespace Connected.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Connected.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Connected.Models.ApplicationDbContext context)
        {


          
       

            /*var userPosts = new List<UserPost>
            {
                
                new UserPost { ApplicationUser Author = "", Body = "Hallo",   Likes = 0, 
                    DateTimePosted = DateTime.Now, Dislikes = 0, Shares = 0, IsImage = false, IsRecipe = false,},
                
            };
            userPosts.ForEach(s => context.UserPosts.AddOrUpdate(p => p.Body, s));
            context.SaveChanges();*/





            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
