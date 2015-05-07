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
            ContextKey = "Connected.Models.ApplicationDbContext";
        }

        protected override void Seed( Connected.Models.ApplicationDbContext context)
        {

            context.UserStatuses.AddOrUpdate(
                
                p => p.Id,
                new UserStatus { Id = 5, Body = "bla"},
                new UserStatus { Id = 6, Body = "blabla"}

                
                
                
                );
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
