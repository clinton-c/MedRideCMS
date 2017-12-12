namespace MedRideCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedStatesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Florida', 'FL', 'True')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Texas', 'TX', 'True')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Michigan', 'MI', 'True')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('California', 'CA', 'True')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('New York', 'NY', 'True')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Colorado', 'CO', 'True')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Pennsylvania', 'PA', 'True')");
    }

        public override void Down()
        {
        }
    }
}
