namespace MedRideCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixtheabbreviatednamesofMinnesotaMississippiMissouriMontana : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE States SET AbbreviatedName = 'MN' WHERE Name = 'Minnesota'");
            Sql("UPDATE States SET AbbreviatedName = 'MS' WHERE Name = 'Mississippi'");
            Sql("UPDATE States SET AbbreviatedName = 'MO' WHERE Name = 'Missouri'");
            Sql("UPDATE States SET AbbreviatedName = 'MT' WHERE Name = 'Montana'");

        }

        public override void Down()
        {
        }
    }
}
