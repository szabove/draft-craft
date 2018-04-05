namespace DraftCraft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IzmjenaVSDProizvodaSaDecimalNaInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Proizvod", "Sirina", c => c.Int(nullable: false));
            AlterColumn("dbo.Proizvod", "Visina", c => c.Int(nullable: false));
            AlterColumn("dbo.Proizvod", "Dubina", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proizvod", "Dubina", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Proizvod", "Visina", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Proizvod", "Sirina", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
