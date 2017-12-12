namespace MedRideCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixissueofhavingmultiplestateswithabbreviatednameAK : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE States SET AbbreviatedName = 'AR' WHERE Name = 'Arkansas'");
        }
        
        public override void Down()
        {
        }
    }
}
