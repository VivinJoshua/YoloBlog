namespace YoloBlogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikesDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.UserDetails", t => t.UserId, cascadeDelete: false)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Post = c.String(nullable: false),
                        Like = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.UserDetails", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfileDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfileDetails", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.LikesDetails", "UserId", "dbo.UserDetails");
            DropForeignKey("dbo.LikesDetails", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "UserId", "dbo.UserDetails");
            DropIndex("dbo.UserProfileDetails", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.LikesDetails", new[] { "UserId" });
            DropIndex("dbo.LikesDetails", new[] { "PostId" });
            DropTable("dbo.UserProfileDetails");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Posts");
            DropTable("dbo.LikesDetails");
        }
    }
}
