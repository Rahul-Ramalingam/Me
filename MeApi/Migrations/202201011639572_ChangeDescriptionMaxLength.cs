namespace MeApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDescriptionMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MainTasks", "MainTaskDescription", c => c.String(maxLength: 2000));
            AlterColumn("dbo.SubTasks", "SubTaskDescription", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubTasks", "SubTaskDescription", c => c.String());
            AlterColumn("dbo.MainTasks", "MainTaskDescription", c => c.String());
        }
    }
}
