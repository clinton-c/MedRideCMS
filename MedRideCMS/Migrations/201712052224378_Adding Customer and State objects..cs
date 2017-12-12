namespace MedRideCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCustomerandStateobjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        StateId = c.Int(nullable: false),
                        City = c.String(),
                        Zip = c.String(),
                        Phone = c.String(),
                        SecondaryPhone = c.String(),
                        Email = c.String(),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AbbreviatedName = c.String(),
                        HasCoverage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "StateId", "dbo.States");
            DropIndex("dbo.Customers", new[] { "StateId" });
            DropTable("dbo.States");
            DropTable("dbo.Customers");
        }
    }
}
