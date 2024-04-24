namespace Niqash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLovesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loves", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Loves", "PostId", "dbo.Posts");
            DropIndex("dbo.Loves", new[] { "PostId" });
            DropIndex("dbo.Loves", new[] { "UserId" });
            DropTable("dbo.Loves");
        }
    }
}
