namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addObjettable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Objets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        objetNom = c.String(),
                        objetDescription = c.String(),
                        objetPrixDepart = c.Double(nullable: false),
                        objetDateInsc = c.DateTime(nullable: false),
                        objetDureeVente = c.Int(nullable: false),
                        objetDateAchat = c.DateTime(nullable: false),
                        objetImage = c.String(),
                        categoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.categoryId, cascadeDelete: true)
                .Index(t => t.categoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Objets", "categoryId", "dbo.Categories");
            DropIndex("dbo.Objets", new[] { "categoryId" });
            DropTable("dbo.Objets");
        }
    }
}
