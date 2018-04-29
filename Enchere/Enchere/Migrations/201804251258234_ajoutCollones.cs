namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutCollones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Civilite", c => c.String());
            AddColumn("dbo.AspNetUsers", "Prenom", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserType", c => c.String());
            AddColumn("dbo.AspNetUsers", "Langue", c => c.String());
            AddColumn("dbo.AspNetUsers", "Tel", c => c.String());
            AddColumn("dbo.AspNetUsers", "Adresse", c => c.String());
            AddColumn("dbo.AspNetUsers", "DateIns", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateIns");
            DropColumn("dbo.AspNetUsers", "Adresse");
            DropColumn("dbo.AspNetUsers", "Tel");
            DropColumn("dbo.AspNetUsers", "Langue");
            DropColumn("dbo.AspNetUsers", "UserType");
            DropColumn("dbo.AspNetUsers", "Prenom");
            DropColumn("dbo.AspNetUsers", "Civilite");
        }
    }
}
