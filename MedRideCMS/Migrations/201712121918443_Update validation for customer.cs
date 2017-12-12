namespace MedRideCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatevalidationforcustomer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Zip", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "SecondaryPhone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "SecondaryPhone", c => c.String(maxLength: 255));
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "Zip", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
