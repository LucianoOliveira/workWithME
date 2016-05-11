using System.Data.Entity;

namespace workWithME.Models
{
    public class projectsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<workWithME.Models.projectsContext>());

        public projectsContext() : base("name=projectsContext")
        {
            //Database.SetInitializer<projectsContext>(null);
        }
        
        //Comment this if it is needed to create new controller using this context
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<projectsContext, Migrations.Configuration>());
        }

        public DbSet<projects> projects { get; set; }

        public DbSet<candidate> candidates { get; set; }

        public DbSet<clients> clients { get; set; }

        public DbSet<locations> locations { get; set; }
    }
}
