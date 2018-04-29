namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addenchira : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Encherees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        enchereNiveau = c.Double(nullable: false),
                        Message = c.String(),
                        enchereDate = c.DateTime(nullable: false),
                        ObjetId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objets", t => t.ObjetId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ObjetId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Encherees", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Encherees", "ObjetId", "dbo.Objets");
            DropIndex("dbo.Encherees", new[] { "UserId" });
            DropIndex("dbo.Encherees", new[] { "ObjetId" });
            DropTable("dbo.Encherees");
        }
    }
}
