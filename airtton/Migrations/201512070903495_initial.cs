namespace airtton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.GroupIntro",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Description = c.String(),
                    ImageUrl = c.String(),
                    ParentId = c.Int(nullable: false),
                    Lan = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Honor",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Description = c.String(),
                    ImageUrl = c.String(),
                    ParentId = c.Int(nullable: false),
                    Lan = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);


            CreateTable(
                "dbo.Organization",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Description = c.String(),
                    ImageUrl = c.String(),
                    ParentId = c.Int(nullable: false),
                    Lan = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.PresidentDetail",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Position = c.String(),
                    Description = c.String(),
                    Facebook = c.String(),
                    Twitter = c.String(),
                    Google = c.String(),
                    LinkedIn = c.String(),
                    Email = c.String(),
                    Skype = c.String(),
                    ImageUrl = c.String(),
                    ParentId = c.Int(nullable: false),
                    Lan = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("dbo.PresidentDetail");
            DropTable("dbo.Organization");
            DropTable("dbo.Honor");
            DropTable("dbo.GroupIntro");

        }
    }
}
