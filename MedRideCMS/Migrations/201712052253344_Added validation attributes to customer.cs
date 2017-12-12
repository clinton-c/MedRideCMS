namespace MedRideCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedvalidationattributestocustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "FirstName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "LastName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "City", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "Zip", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "SecondaryPhone", c => c.String(maxLength: 255));
            AlterColumn("dbo.Customers", "Email", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "SecondaryPhone", c => c.String());
            AlterColumn("dbo.Customers", "Phone", c => c.String());
            AlterColumn("dbo.Customers", "Zip", c => c.String());
            AlterColumn("dbo.Customers", "City", c => c.String());
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "LastName", c => c.String());
            AlterColumn("dbo.Customers", "FirstName", c => c.String());
            DropColumn("dbo.Customers", "Created");
        }
    }
}
