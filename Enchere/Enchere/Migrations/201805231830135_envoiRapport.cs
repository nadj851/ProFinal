namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class envoiRapport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "dernierEnvoiRapport", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "dernierEnvoiRapport");
        }
    }
}
