namespace MedRideCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDistrictofColumbia : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Distric of Columbia', 'DC', 'False')");
        }
        
        public override void Down()
        {
        }
    }
}
