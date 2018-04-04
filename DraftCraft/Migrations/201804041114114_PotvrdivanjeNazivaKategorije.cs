namespace DraftCraft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PotvrdivanjeNazivaKategorije : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kategorija", "Naziv", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kategorija", "Naziv", c => c.String());
        }
    }
}
