namespace Niqash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeAboutMeMaxLength200 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "AboutMe", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "AboutMe", c => c.String(maxLength: 500));
        }
    }
}
