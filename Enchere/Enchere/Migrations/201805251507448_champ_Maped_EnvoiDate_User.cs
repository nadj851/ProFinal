namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class champ_Maped_EnvoiDate_User : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "dernierEnvoiRapport");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "dernierEnvoiRapport", c => c.DateTime(nullable: false));
        }
    }
}
