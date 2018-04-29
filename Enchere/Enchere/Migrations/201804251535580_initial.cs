namespace Enchere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "DateIns");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DateIns", c => c.String());
        }
    }
}
