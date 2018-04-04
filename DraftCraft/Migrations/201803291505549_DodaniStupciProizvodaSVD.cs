namespace DraftCraft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodaniStupciProizvodaSVD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proizvod", "Sirina", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Proizvod", "Visina", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Proizvod", "Dubina", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Proizvod", "Dubina");
            DropColumn("dbo.Proizvod", "Visina");
            DropColumn("dbo.Proizvod", "Sirina");
        }
    }
}
