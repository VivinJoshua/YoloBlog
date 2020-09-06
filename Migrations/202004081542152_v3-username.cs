namespace YoloBlogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3username : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDetails", "UserName");
        }
    }
}
