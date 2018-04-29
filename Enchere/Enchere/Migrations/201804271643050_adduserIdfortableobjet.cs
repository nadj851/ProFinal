namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserIdfortableobjet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objets", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Objets", "UserId");
            AddForeignKey("dbo.Objets", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Objets", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Objets", new[] { "UserId" });
            DropColumn("dbo.Objets", "UserId");
        }
    }
}
