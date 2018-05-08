namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modtableeval : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Evaluations", "Commentaire", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Evaluations", "Commentaire", c => c.String());
        }
    }
}
