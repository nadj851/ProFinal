namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutColloneTotalcote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evaluations", "TotalCote", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Evaluations", "TotalCote");
        }
    }
}
