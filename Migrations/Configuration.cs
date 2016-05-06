namespace workWithME.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using workWithME.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<workWithME.Models.projectsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(workWithME.Models.projectsContext context)
        {
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
            string[] clientArr = { "HP", "BLIP", "EDP", "PT", "SAPO", "NOS" };
            string[] locationArr = { "Lisboa", "Porto", "Coimbra", "Braga"};
            string[] descArr = { "WebDev", "DB Admin", "Programmer", "Team Leader" };
            var r = new Random();
            var items = Enumerable.Range(1, 50).Select(o => new projects
            {
                priorityNum = r.Next(10),
                clientId= (int)r.Next(5),
                locationId = (int)r.Next(3),
                numCandidates = r.Next(20),
                numVacancies = r.Next(20),   
                dueDate = new DateTime(2017, r.Next(1, 12), r.Next(1, 28)),
                active = (byte)r.Next(2),
                project = o.ToString()
            }).ToArray();
            context.projects.AddOrUpdate(item => new { item.project }, items);
        }
    }
}
