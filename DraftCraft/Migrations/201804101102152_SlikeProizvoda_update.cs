namespace DraftCraft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SlikeProizvoda_update : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SlikaProizvodas", newName: "SlikeProizvoda");
            AddColumn("dbo.SlikeProizvoda", "ProizvodID", c => c.Int(nullable: false));
            CreateIndex("dbo.SlikeProizvoda", "ProizvodID");
            AddForeignKey("dbo.SlikeProizvoda", "ProizvodID", "dbo.Proizvod", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SlikeProizvoda", "ProizvodID", "dbo.Proizvod");
            DropIndex("dbo.SlikeProizvoda", new[] { "ProizvodID" });
            DropColumn("dbo.SlikeProizvoda", "ProizvodID");
            RenameTable(name: "dbo.SlikeProizvoda", newName: "SlikaProizvodas");
        }
    }
}
