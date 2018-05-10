namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutchampStatutObjet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objets", "Statut", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Objets", "Statut");
        }
    }
}
