namespace ImageCloudDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exir : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        ImageDate = c.DateTime(nullable: false),
                        FileAdress = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        UserPassword = c.String(),
                        IsEmailVerified = c.Boolean(nullable: false),
                        IsBanned = c.Boolean(nullable: false),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Images");
        }
    }
}
