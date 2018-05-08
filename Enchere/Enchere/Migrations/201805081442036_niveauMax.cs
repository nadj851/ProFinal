namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class niveauMax : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Encherees", "niveauMax", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Encherees", "niveauMax");
        }
    }
}
