namespace workWithME.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using workWithME.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<workWithME.Models.projectsContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
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

            
            string[] clientArr = { "", "HP", "BLIP", "EDP", "PT", "SAPO", "NOS" };
            string[] locationArr = { "", "Lisboa", "Porto", "Coimbra", "Braga", "Faro"};
            string[] descArr = { "", "WebDev", "DB Admin", "C# Programmer", "Team Leader", "RPG AS400 Programmer", "JAVA Programmer", "Python Programmer", "PL/SQL Dev", "Dev Mobile iOS", "Dev Mobile Android" };
            string[] prioArr = { "", "1 - Very Low", "2 - Low", "3 - Medium", "4 - High", "5 - Critical" };
            var r = new Random();

            //*
            //Projects
            //*
            IList<projects> defaultProjects = new List<projects>();
            for (int i = 0; i < 50; i++)
            {
                var cliRand = r.Next(1,5);
                var locRand = r.Next(1,5);
                var descRand = r.Next(1, 10);
                var prioRand = r.Next(1, 5);
                defaultProjects.Add(new projects()
                {
                    priorityNum = prioRand,
                    priority = prioArr[prioRand],
                    clientId = cliRand,
                    client = clientArr[cliRand],
                    locationId = locRand,
                    location = locationArr[locRand],
                    numCandidates = r.Next(20),
                    numVacancies = r.Next(20),
                    dueDate = new DateTime(2017, r.Next(1, 12), r.Next(1, 28)),
                    active = (byte)r.Next(2),
                    description = descArr[descRand],
                    project = clientArr[cliRand]+" - "+descArr[descRand]
                });
            }
            foreach (projects std in defaultProjects)
                context.projects.AddOrUpdate(std);

            //*
            //Clients
            //*
            IList<clients> defaultClients = new List<clients>();
            for (int i = 1; i < 7; i++)
            {
                defaultClients.Add(new clients()
                {
                    clientID = i,
                    clientName = clientArr[i]
                });
            }
            foreach (clients clt in defaultClients)
                context.clients.AddOrUpdate(clt);

            //*
            //Locations
            //*
            IList<locations> defaultLocations = new List<locations>();
            for (int i = 1; i < 6; i++)
            {
                defaultLocations.Add(new locations()
                {
                    locationID = i,
                    locationName = locationArr[i]
                });
            }
            foreach (locations locat in defaultLocations)
                context.locations.AddOrUpdate(locat);


            base.Seed(context);
        }
    }
}
