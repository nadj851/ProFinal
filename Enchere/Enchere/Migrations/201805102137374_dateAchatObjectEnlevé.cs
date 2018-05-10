namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateAchatObjectEnlevé : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Objets", "objetDateAchat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Objets", "objetDateAchat", c => c.DateTime(nullable: false));
        }
    }
}
