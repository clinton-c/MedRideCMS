namespace MedRideCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seedmorestates : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Alabama', 'AL', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Alaska', 'AK', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Arizona', 'AZ', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Arkansas', 'AK', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Connecticut', 'CT', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Delaware', 'DE', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Georgia', 'GA', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Hawaii', 'HI', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Idaho', 'ID', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Illinois', 'IL', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Indiana', 'IN', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Iowa', 'IA', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Kansas', 'KS', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Kentucky', 'KY', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Louisiana', 'LA', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Maine', 'ME', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Maryland', 'MD', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Massachusetts', 'MA', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Minnesota', 'MI', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Mississippi', 'MN', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Missouri', 'MS', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Montana', 'MO', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Nebraska', 'NE', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Nevada', 'NV', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('New Hampshire', 'NH', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('New Jersey', 'NJ', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('New Mexico', 'NM', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('North Carolina', 'NC', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('North Dakota', 'ND', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Ohio', 'OH', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Oklahoma', 'OK', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Oregon', 'OR', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Rhode Island', 'RI', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('South Carolina', 'SC', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('South Dakota', 'SD', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Tennessee', 'TN', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Utah', 'UT', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Vermont', 'VT', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Virginia', 'VA', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Washington', 'WA', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('West Virginia', 'WV', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Wisconsin', 'WI', 'False')");
            Sql("INSERT INTO States (Name, AbbreviatedName, HasCoverage) VALUES ('Wyoming', 'WY', 'False')");
        }
        
        public override void Down()
        {
        }
    }
}
