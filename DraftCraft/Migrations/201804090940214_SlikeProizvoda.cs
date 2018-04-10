namespace DraftCraft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SlikeProizvoda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SlikaProizvodas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImeDatoteke = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SlikaProizvodas");
        }
    }
}
