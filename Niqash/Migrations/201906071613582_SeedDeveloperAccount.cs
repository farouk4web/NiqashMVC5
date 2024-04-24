namespace Niqash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedDeveloperAccount : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [AboutMe], [ProfilePicSrc]) VALUES (N'bc715bc9-31c9-41b2-a83b-e18638654b71', N'farouk@niqash.com', 1, N'AJJuPaCaFT6IHdT15chO44w87KFpNrQhxzpPmQ7lyEKwPWF6UcQWm21uHoIo2oQciA==', N'2ab54b70-a541-42f1-b5e1-57fd115849dd', NULL, 0, 0, NULL, 1, 0, N'farouk@niqash.com', N'Farouk', N'Abdelhamid', N'hi i am Farouk', N'/Content/user.jpg')");
        }

        public override void Down()
        {
        }
    }
}
