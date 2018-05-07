namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIntToLongObjectDureeVente : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Objets", "objetDureeVente", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Objets", "objetDureeVente", c => c.Int(nullable: false));
        }
    }
}
