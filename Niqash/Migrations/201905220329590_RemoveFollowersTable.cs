namespace Niqash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFollowersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Followers", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Followers", new[] { "AccountId" });
            DropIndex("dbo.Followers", new[] { "UserId" });
            DropTable("dbo.Followers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Followers", "UserId");
            CreateIndex("dbo.Followers", "AccountId");
            AddForeignKey("dbo.Followers", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Followers", "AccountId", "dbo.AspNetUsers", "Id");
        }
    }
}
