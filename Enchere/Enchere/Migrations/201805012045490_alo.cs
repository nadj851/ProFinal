namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alo : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.tests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
