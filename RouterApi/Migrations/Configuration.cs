namespace RouterApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RouterApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<RouterApi.Data.RouterApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RouterApi.Data.RouterApiContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Users.AddOrUpdate(u => u.Id,
            new Users() { Id = 1, Name = "Rahul", UserName = "@rahulram", UltimateGoal = "Millionare" }
                );
            context.MainTasks.AddOrUpdate(mt => mt.Id,
                new MainTasks() { Id = 1, MainTaskName = "Dot Net Web API", MainTaskDescription = "Learn Asp dot net web API concepts with Entity Framework", StartTime = DateTime.Today, EndTime = DateTime.Today.AddMonths(1), UserId = 1 }
                );
            context.SubTasks.AddOrUpdate(st => st.Id,
                new SubTasks() { Id = 1, SubTaskName = "CRUD operations", SubTaskDescription = "Perform CRUD operations using web API", StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(4), MainTaskId = 1 }
                );

        }
    }
}
