namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutEntiteEvaluation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evaluations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateEvaluation = c.DateTime(nullable: false),
                        Cote = c.Double(nullable: false),
                        Commentaire = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evaluations", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Evaluations", new[] { "UserId" });
            DropTable("dbo.Evaluations");
        }
    }
}
