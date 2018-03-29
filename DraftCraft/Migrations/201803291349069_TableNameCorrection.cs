namespace DraftCraft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNameCorrection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategorija",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Proizvod",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Opis = c.String(),
                        Cijena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KategorijaID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kategorija", t => t.KategorijaID)
                .Index(t => t.KategorijaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proizvod", "KategorijaID", "dbo.Kategorija");
            DropIndex("dbo.Proizvod", new[] { "KategorijaID" });
            DropTable("dbo.Proizvod");
            DropTable("dbo.Kategorija");
        }
    }
}
