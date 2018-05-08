namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modtableeval2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evaluations", "Vendeur", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Evaluations", "Vendeur");
        }
    }
}
