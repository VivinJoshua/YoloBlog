namespace YoloBlogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2Added_Date_Fi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "UserProfileDetailss_Id", c => c.Int());
            CreateIndex("dbo.Posts", "UserProfileDetailss_Id");
            AddForeignKey("dbo.Posts", "UserProfileDetailss_Id", "dbo.UserProfileDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserProfileDetailss_Id", "dbo.UserProfileDetails");
            DropIndex("dbo.Posts", new[] { "UserProfileDetailss_Id" });
            DropColumn("dbo.Posts", "UserProfileDetailss_Id");
        }
    }
}
