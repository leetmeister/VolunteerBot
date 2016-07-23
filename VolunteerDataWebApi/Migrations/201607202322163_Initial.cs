namespace VolunteerDataWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDateTime = c.DateTime(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VolunteerActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDateTime = c.DateTime(nullable: false),
                        Duration = c.Time(precision: 7),
                        ReportingProperties = c.Int(nullable: false),
                        VolunteerId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Volunteers", t => t.VolunteerId, cascadeDelete: true)
                .Index(t => t.VolunteerId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VolunteerIntents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDateTime = c.DateTime(),
                        Duration = c.Time(precision: 7),
                        Intent = c.Int(nullable: false),
                        VolunteerId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Volunteers", t => t.VolunteerId, cascadeDelete: true)
                .Index(t => t.VolunteerId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VolunteerIntents", "VolunteerId", "dbo.Volunteers");
            DropForeignKey("dbo.VolunteerIntents", "EventId", "dbo.Events");
            DropForeignKey("dbo.VolunteerActivities", "VolunteerId", "dbo.Volunteers");
            DropForeignKey("dbo.VolunteerActivities", "EventId", "dbo.Events");
            DropIndex("dbo.VolunteerIntents", new[] { "EventId" });
            DropIndex("dbo.VolunteerIntents", new[] { "VolunteerId" });
            DropIndex("dbo.VolunteerActivities", new[] { "EventId" });
            DropIndex("dbo.VolunteerActivities", new[] { "VolunteerId" });
            DropTable("dbo.VolunteerIntents");
            DropTable("dbo.Volunteers");
            DropTable("dbo.VolunteerActivities");
            DropTable("dbo.Events");
        }
    }
}
