namespace CallForwarding.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Senators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        State_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.State_Id)
                .Index(t => t.State_Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zipcodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZipcodeNumber = c.Int(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Senators", "State_Id", "dbo.States");
            DropIndex("dbo.Senators", new[] { "State_Id" });
            DropTable("dbo.Zipcodes");
            DropTable("dbo.States");
            DropTable("dbo.Senators");
        }
    }
}
