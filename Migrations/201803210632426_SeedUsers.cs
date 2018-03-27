namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5a474e38-6cf6-46f5-a83f-1c037e4d142b', N'guest@vidly.com', 0, N'ANr/Hdi2FMpXwN1oHl+NiK68SLr2mej6R591Mmkd3K90kOBsbOMTpr9OZfgYSk6NsA==', N'6bb18635-77cb-4a5c-86c0-e478b71c98fe', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7a0ce636-06b3-4c19-9e65-72f716883e79', N'admin@vidly.om', 0, N'AK5adi07Ww6fhOlsAxcMMtL/JlC3wb1PHgI04m1WIoGdrfo+L4KjnlOHVCBChM6npw==', N'2df1176f-384a-4db9-8585-b897bc7f03d4', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.om')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd47c9e83-67db-4e03-a110-f008bfb9f992', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7a0ce636-06b3-4c19-9e65-72f716883e79', N'd47c9e83-67db-4e03-a110-f008bfb9f992')

                ");
        }
        
        public override void Down()
        {
        }
    }
}
