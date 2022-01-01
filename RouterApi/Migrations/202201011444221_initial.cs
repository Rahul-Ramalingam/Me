namespace RouterApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainTasks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    MainTaskName = c.String(nullable: false),
                    MainTaskDescription = c.String(),
                    StartTime = c.DateTime(nullable: false),
                    EndTime = c.DateTime(nullable: false),
                    UserId = c.Int(nullable: false),
                    Users_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Users_Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false),
                    Name = c.String(),
                    UltimateGoal = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SubTasks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SubTaskName = c.String(nullable: false),
                    SubTaskDescription = c.String(),
                    StartTime = c.DateTime(nullable: false),
                    EndTime = c.DateTime(nullable: false),
                    MainTaskId = c.Int(nullable: false),
                    MainTasks_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainTasks", t => t.MainTasks_Id)
                .Index(t => t.MainTasks_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.SubTasks", "MainTasks_Id", "dbo.MainTasks");
            DropForeignKey("dbo.MainTasks", "Users_Id", "dbo.Users");
            DropIndex("dbo.SubTasks", new[] { "MainTasks_Id" });
            DropIndex("dbo.MainTasks", new[] { "Users_Id" });
            DropTable("dbo.SubTasks");
            DropTable("dbo.Users");
            DropTable("dbo.MainTasks");
        }
    }
}
