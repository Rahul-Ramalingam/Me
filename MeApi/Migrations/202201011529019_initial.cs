namespace MeApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainTasks",
                c => new
                    {
                        MainTaskId = c.Int(nullable: false, identity: true),
                        MainTaskName = c.String(nullable: false),
                        MainTaskDescription = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MainTaskId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false,maxLength : 1000),
                        Name = c.String(),
                        UltimateGoal = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.SubTasks",
                c => new
                    {
                        SubTaskId = c.Int(nullable: false, identity: true),
                        SubTaskName = c.String(nullable: false),
                        SubTaskDescription = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        MainTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubTaskId)
                .ForeignKey("dbo.MainTasks", t => t.MainTaskId, cascadeDelete: true)
                .Index(t => t.MainTaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubTasks", "MainTaskId", "dbo.MainTasks");
            DropForeignKey("dbo.MainTasks", "UserId", "dbo.Users");
            DropIndex("dbo.SubTasks", new[] { "MainTaskId" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.MainTasks", new[] { "UserId" });
            DropTable("dbo.SubTasks");
            DropTable("dbo.Users");
            DropTable("dbo.MainTasks");
        }
    }
}
