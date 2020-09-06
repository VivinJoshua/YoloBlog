namespace YoloBlogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2Added_Date_Field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "PostDateTime");
        }
    }
}
