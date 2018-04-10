namespace DraftCraft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnikatneSlike : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SlikaProizvodas", "ImeDatoteke", c => c.String(maxLength: 100));
            CreateIndex("dbo.SlikaProizvodas", "ImeDatoteke", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.SlikaProizvodas", new[] { "ImeDatoteke" });
            AlterColumn("dbo.SlikaProizvodas", "ImeDatoteke", c => c.String());
        }
    }
}
