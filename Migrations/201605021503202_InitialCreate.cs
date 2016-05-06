namespace workWithME.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.projects",
                c => new
                    {
                        projectId = c.Int(nullable: false, identity: true),
                        project = c.String(),
                        priorityNum = c.Int(nullable: false),
                        priority = c.String(),
                        clientId = c.Int(nullable: false),
                        client = c.String(),
                        locationId = c.Int(nullable: false),
                        location = c.String(),
                        description = c.String(),
                        numCandidates = c.Int(nullable: false),
                        numVacancies = c.Int(nullable: false),
                        dueDate = c.DateTime(),
                        active = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.projectId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.projects");
        }
    }
}
