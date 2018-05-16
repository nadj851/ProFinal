namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Objets", "Statut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Objets", "Statut", c => c.String());
        }
    }
}
