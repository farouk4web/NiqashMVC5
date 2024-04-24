namespace Niqash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeAboutMeAsOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "AboutMe", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "AboutMe", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
